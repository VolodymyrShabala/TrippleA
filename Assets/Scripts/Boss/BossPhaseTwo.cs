using UnityEngine;
using System.Collections;

public class BossPhaseTwo : MonoBehaviour, IDamageable {
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite damagedSprite;
    private Sprite defaultSprite;

    [Header("HP")]
    [SerializeField]
    private int health = 10;

    [Header("On death")]
    [SerializeField]
    private Boss nextBoss;

    private bool invincible = true;

    private void Start() {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
        StartCoroutine("Spawn");
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            spriteRenderer.sprite = damagedSprite;
        }
    }

    public void TakeDamage(int takenDamage = 1) {
        if(invincible) {
            return;
        }

        print("!");
        anim.SetTrigger("TakeDamage");
        health -= takenDamage;
        //StartCoroutine("TakenDamageVisualise");
        if(health <= 0) {
            Die();
        }
    }

    private IEnumerator TakenDamageVisualise() {
        spriteRenderer.sprite = damagedSprite;
        print(Time.time);
        yield return new WaitForSeconds(0.1f);
        //spriteRenderer.sprite = defaultSprite;
        //print(Time.time);
        yield return null;
    }

    public void Die() {
        invincible = true;
        StartCoroutine("Death");
        anim.SetBool("Dead", true);
    }

    private IEnumerator Death() {
        yield return null;
    }

    private IEnumerator SpawnInNextBoss() {
        anim.SetBool("Dead", true);
        if(nextBoss) {
            Instantiate(nextBoss, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(6.0f);
        Destroy(gameObject);
    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(3.0f);
        anim.SetTrigger("Spawn");
        invincible = false;
    }
}