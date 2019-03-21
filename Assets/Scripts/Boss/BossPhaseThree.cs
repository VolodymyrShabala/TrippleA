using System.Collections;
using UnityEngine;

public class BossPhaseThree : MonoBehaviour {
    private Transform player;
    private Animator anim;

    [Header("HP")]
    [SerializeField]
    private int health = 10;

    [Header("Attack with two swords")]
    [SerializeField]
    private Projectile swordAttackProjectile;
    [SerializeField]
    private Transform[] swordAttackVisualSpawn = new Transform[2];
    [Header("Attack with unlimited blade works")]
    [SerializeField]
    private Projectile swordUnlimitedProjectile;
    [SerializeField]
    private Transform placeToSpawnSwords;

    [Header("Attack beam")]
    [SerializeField]
    private Projectile beamProjectile;
    [SerializeField]
    private int beamLength = 30;
    [SerializeField]
    private float timeBetweenBeams = 0.05f;

    [Header("Attack properties")]
    [SerializeField]
    private int damage = 1;
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
            int r = Random.Range(0, 3);
            switch(r) {
                case 0:
                    anim.SetTrigger("AttackWithSwords");
                    break;
                case 1:
                    anim.SetTrigger("AttackWithUnlimitedBladeWorks");
                    break;
                case 2:
                    StartCoroutine(AttackWithBeam());
                    break;
                default:
                    Debug.Log("Boss three should never get in to this state");
                    break;
            }
            attackSpeedHolder = Time.time + attackSpeed;
        }
    }

    private IEnumerator AttackWithBeam() {
        anim.SetBool("AttackBeam", true);
        for(int i = 0; i < beamLength; i++) {
            Projectile pr = Instantiate(beamProjectile, placeToSpawnSwords.position, Quaternion.identity);
            pr.MoveTo(player.position);
            pr.Damage = damage;
            pr.IgnoreCollision(gameObject);

            yield return new WaitForSeconds(timeBetweenBeams);
        }
        anim.SetBool("AttackBeam", false);
        yield return null;
    }

    private void AttackWithUnlimitedBladeWorks() {
        Projectile pr = Instantiate(swordUnlimitedProjectile, placeToSpawnSwords.position, Quaternion.identity);
        pr.MoveTo(player.position);
        pr.Damage = damage;
    }

    private void AttackWithSwords() {
        for(int i = 0; i < swordAttackVisualSpawn.Length; i++) {
            Projectile pr = Instantiate(swordAttackProjectile, swordAttackVisualSpawn[i].position, Quaternion.identity);
            pr.MoveTo(player.position);
            pr.Damage = damage;
        }
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
        if(dead) {
            return;
        }
        dead = true;
        anim.SetTrigger("Death");
        StartCoroutine("SpawnExplosionEnum");
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
            Vector2 point = Random.insideUnitCircle * 6f;
            Vector2 pos = new Vector3(transform.position.x + point.x, transform.position.y + point.y, transform.position.z);
            Instantiate(explosion, pos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenExplosions);

        }
        GetComponent<SpriteRenderer>().enabled = false;
        yield return null;
    }
}