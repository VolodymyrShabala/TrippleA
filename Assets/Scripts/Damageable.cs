using UnityEngine;

public class Damageable : MonoBehaviour {
    [SerializeField] [Tooltip("Add in order: most damaged to non damaged")]
    private Sprite[,] sprites = new Sprite[5, 4];

    //[SerializeField]
    //private Sprite[] defaultSprites = new Sprite[4];
    //[SerializeField]
    //private Sprite[] smallDamagedSprites = new Sprite[4];
    //[SerializeField]
    //private Sprite[] moderatelyDamagedSprites = new Sprite[4];
    //[SerializeField]
    //private Sprite[] mediumDamagedSprites = new Sprite[4];
    //[SerializeField]
    //private Sprite[] heavyDamagedSprites = new Sprite[4];

    [SerializeField]
    private int health = 5;

    private void Start() {
        UpdateVisuals();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            TakeDamage();
        }
    }

    public void TakeDamage(int damage = 1) {
        health -= damage;
        UpdateVisuals();
    }

    private void UpdateVisuals() {
        for(int i = transform.childCount; i == 0; --i) {
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = sprites[health, i];
        }
        //switch(health) {
        //    case 5:
        //        for(int i = 0; i < transform.childCount; i++) {
        //            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = defaultSprites[i];
        //        }
        //        break;
        //    case 4:
        //        for(int i = 0; i < transform.childCount; i++) {
        //            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = smallDamagedSprites[i];
        //        }
        //        break;
        //    case 3:
        //        for(int i = 0; i < transform.childCount; i++) {
        //            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = moderatelyDamagedSprites[i];
        //        }
        //        break;
        //    case 2:
        //        for(int i = 0; i < transform.childCount; i++) {
        //            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = mediumDamagedSprites[i];
        //        }
        //        break;
        //    case 1:
        //        for(int i = 0; i < transform.childCount; i++) {
        //            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = heavyDamagedSprites[i];
        //        }
        //        break;
        //}
    }
}