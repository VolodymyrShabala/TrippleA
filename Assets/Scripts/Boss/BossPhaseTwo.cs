using UnityEngine;
using System.Collections;

public class BossPhaseTwo : MonoBehaviour, IDamageable {
    private Animator anim;


    [Header("HP")]
    [SerializeField]
    private int health = 10;

    [Header("On death")]
    [SerializeField]
    private GameObject nextBoss;

    private bool invincible = true;

    private void Start() {
        anim = GetComponent<Animator>();
        StartCoroutine("Spawn");
    }

    public void TakeDamage(int takenDamage = 1) {
        if(invincible) {
            return;
        }

        anim.SetTrigger("TakeDamage");
        health -= takenDamage;
        //StartCoroutine("TakenDamageVisualise");
        if(health <= 0) {
            Die();
        }
    }

    public void Die() {
        invincible = true;
        StartCoroutine("Death");
        anim.SetBool("Dead", true);
    }

    private IEnumerator Death() {
        yield return new WaitForSeconds(3.0f);
        if(nextBoss) {
            Instantiate(nextBoss, transform.position - Vector3.up, Quaternion.identity);
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