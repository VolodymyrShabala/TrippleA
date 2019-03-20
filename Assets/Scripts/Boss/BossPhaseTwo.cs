using UnityEngine;
using System.Collections;

public class BossPhaseTwo : MonoBehaviour {
    private Animator anim;

    [Header("HP")]
    [SerializeField]
    private int health = 10;

    [Header("On death")]
    [SerializeField]
    private Boss nextBoss;


    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int takenDamage = 1) {
        health -= takenDamage;
        anim.SetTrigger("TakeDamage");
        if(health <= 0) {
            Die();
        }
    }

    public void Die() {
        StartCoroutine("SpawnExplosionEnum");
    }

    private IEnumerator SpawnInNextBoss() {
        anim.SetBool("Dead", true);
        if(nextBoss) {
            Instantiate(nextBoss, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(6.0f);
        Destroy(gameObject);
    }
}