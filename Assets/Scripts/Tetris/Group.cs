using UnityEngine;

public class Group : MonoBehaviour {
    private float lastFall = 0;
    [SerializeField]
    private float downPressFallDownSpeed = 0.1f;
    [SerializeField]
    private bool canRotate = true;
    [HideInInspector]
    public bool placed = false;

    private void Start() {
        if(!IsValidGridPosition()) {
            Debug.Log("Game over");
            Destroy(gameObject);
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-1, 0);
            if(IsValidGridPosition()) {
                UpdateGrid();
            } else {
                transform.position += new Vector3(1, 0);
            }
        } else if(Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += new Vector3(1, 0);
            if(IsValidGridPosition()) {
                UpdateGrid();
            } else {
                transform.position += new Vector3(-1, 0);
            }
        } else if(Input.GetKeyDown(KeyCode.UpArrow) && canRotate) {
            transform.Rotate(0, 0, -90);
            if(IsValidGridPosition()) {
                UpdateGrid();
            } else {
                transform.Rotate(0, 0, 90);
            }
        } else if(Input.GetKey(KeyCode.DownArrow) && Time.time - lastFall >= downPressFallDownSpeed|| Time.time - lastFall >= 1) {
            transform.position += new Vector3(0, -1);
            if(IsValidGridPosition()) {
                UpdateGrid();
            } else {
                transform.position += new Vector3(0, 1);
                Grid.DeleteFullRows();
                FindObjectOfType<Spawner>().SpawnNext();
                placed = true;
                enabled = false;
            }
            lastFall = Time.time;
        }
    }

    public void Die() {
        foreach(Transform child in transform) {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = null;
        }
        if(!placed) {
            FindObjectOfType<Spawner>().SpawnNext();
        }
        Destroy(gameObject);
    }

    private void UpdateGrid() {
        for(int y = 0; y < Grid.height; ++y) {
            for(int x = 0; x < Grid.width; ++x) {
                if(Grid.grid[x, y] == null) {
                    continue;
                }
                if(Grid.grid[x, y].parent == transform) {
                    Grid.grid[x, y] = null;
                }
            }
        }

        foreach(Transform child in transform) {
            Vector2 v = Grid.RoundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }

    private bool IsValidGridPosition() {
        foreach(Transform child in transform) {
            Vector2 v = Grid.RoundVec2(child.position);
            if(!Grid.IsInsideBorder(v)) {
                return false;
            }

            if(Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform) {
                return false;
            }
        }
        return true;
    }
}
