using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable {
    public Thing[] sprites = new Thing[4];


    [SerializeField]
    private int health = 4;

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
        for(int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprites[health].sprites[i];
        }
    }
}

[System.Serializable]
public struct Thing {
    public Sprite[] sprites;
}