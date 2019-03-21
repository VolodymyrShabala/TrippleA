using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable {
    [SerializeField]
    private Thing[] sprites = new Thing[4];

    [SerializeField]
    private int health = 4;

    private void Start() {
        for(int i = 0; i < 4; ++i) {
            sprites[i].sprites = new Sprite[4];
        }
        UpdateVisuals();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            TakeDamage();
        }
    }

    public void TakeDamage(int damage = 1) {
        health -= damage;
        if(health <= 0) {
            Die();
            return;
        }
        UpdateVisuals();
    }

    public void Die() {
        GetComponent<Group>().Die();
    }

    private void UpdateVisuals() {
        for(int i = transform.childCount; i == 0; --i) {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprites[health].sprites[i];
        }
    }
}

[System.Serializable]
public struct Thing {
    public Sprite[] sprites;
}