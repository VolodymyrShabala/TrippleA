using System.Collections;
using UnityEngine;

public class BossPhaseThree : MonoBehaviour {
    private Transform player;
    private Animator anim;

    [Header("HP")]
    [SerializeField]
    private int health = 10;

    [Header("Attack")]
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private GameObject swordAttackVisual;
    [SerializeField]
    private Transform shootFromTransform;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int straightShotsAmount = 5;
    [SerializeField]
    private float timeBeetweenShotsAmount = 0.3f;
    [SerializeField]
    private float attacksPerSecond = 10;
    private float attackSpeed;
    private float attackSpeedHolder;

    [Header("On death")]
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int numberOfExplosions = 25;
    [SerializeField]
    private float timeBetweenExplosions = 0.01f;

    private bool invincible = true;
    private bool dead = false;

    private void Start() {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().transform;
        if(attacksPerSecond > 0) {
            attackSpeed = 60 / attacksPerSecond;
        }
        attackSpeedHolder = Time.time + attackSpeed;
    }

    private void Update() {
        if(dead) {
            return;
        }

        if(Time.time >= attackSpeedHolder) {
            StartCoroutine("AttackStraight");
            attackSpeedHolder = Time.time + attackSpeed;
        }
    }

    private IEnumerator AttackStraight() {
        for(int i = 0; i < straightShotsAmount; i++) {
            if(dead) {
                break;
            }

            Projectile pr = Instantiate(projectile, shootFromTransform.position, Quaternion.identity);
            pr.MoveTo(player.position);
            pr.Damage = damage;
            pr.IgnoreCollision(gameObject);
            yield return new WaitForSeconds(timeBeetweenShotsAmount);
        }
        yield return null;
    }

    public void TakeDamage(int takenDamage = 1) {
        if(dead || invincible) {
            return;
        }
        health -= takenDamage;
        if(health <= 0) {
            Die();
        }
    }

    public void Die() {
        dead = true;
        StartCoroutine("SpawnExplosionEnum");
    }

    private void AttackWithSword(Transform fromPosition) {

    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(3.0f);
        anim.SetTrigger("Spawn");
        yield return new WaitForSeconds(5.0f);
        invincible = false;
    }

    private IEnumerator SpawnExplosionEnum() {
        anim.SetBool("Stop", true);
        for(int i = 0; i < numberOfExplosions; i++) {
            Vector2 point = Random.insideUnitCircle * 1.5f;
            Vector2 pos = new Vector3(transform.position.x + point.x, transform.position.y + point.y, transform.position.z);
            Instantiate(explosion, pos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenExplosions);

        }
        yield return null;
    }
}