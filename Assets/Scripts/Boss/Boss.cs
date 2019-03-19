using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable {
    protected Animator anim;

    [SerializeField]
    private int health = 10;


    [SerializeField]
    private Boss nextBoss;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int numberOfExplosions = 25;
    [SerializeField]
    private float timeBetweenExplosions = 0.01f;

    private bool isInvincible = false;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    protected void CanBeDamaged(bool canBeDamaged = true) {
        isInvincible = canBeDamaged;
    }

    public void TakeDamage(int takenDamage = 1) {
        if(isInvincible) {
            return;
        }
        health -= takenDamage;
        anim.SetBool("TookDamage", true);
        if(health <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        StartCoroutine("SpawnExplosionEnum");
    }

    private void SpawnInNextBoss() {
        anim.SetBool("Dead", true);
        if(nextBoss) {
            Instantiate(nextBoss, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void SpawnExplosionsOnDeath() {
        StartCoroutine("SpawnExplosionEnum");
    }

    private IEnumerator SpawnExplosionEnum() {
        for(int i = 0; i < numberOfExplosions; i++) {
            Vector2 point = Random.insideUnitCircle;
            Instantiate(explosion, point, Quaternion.identity);
            new WaitForSeconds(timeBetweenExplosions);
        }
        SpawnInNextBoss();
        return null;
    }
}