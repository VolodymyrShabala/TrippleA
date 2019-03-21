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
    private Projectile swordAttackVisual;
    [SerializeField]
    private Transform[] swordAttackVisualSpawn = new Transform[2];
    [Header("Attack with unlimited blade works")]
    [SerializeField]
    private Projectile swordUnlimitedVisual;
    [SerializeField]
    private Transform placeToSpawnSwords;

    [Header("Attack properties")]
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float timeBetweenAttacks = 0.3f;
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

        if(Input.GetKeyDown(KeyCode.Space)) {
            //anim.SetTrigger("AttackWithSwords");
            anim.SetTrigger("AttackWithUnlimitedBladeWorks");
        }


        //if(Time.time >= attackSpeedHolder) {
        //    StartCoroutine("AttackStraight");
        //    attackSpeedHolder = Time.time + attackSpeed;
        //}
    }

    private void AttackWithUnlimitedBladeWorks() {
        Projectile pr = Instantiate(swordUnlimitedVisual, placeToSpawnSwords.position, Quaternion.identity);
        pr.MoveTo(player.position);
        pr.Damage = damage;
    }

    private void AttackWithSwords() {
        for(int i = 0; i < swordAttackVisualSpawn.Length; i++) {
            Projectile pr = Instantiate(swordAttackVisual, swordAttackVisualSpawn[i].position, Quaternion.identity);
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
        dead = true;
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
        yield return null;
    }
}