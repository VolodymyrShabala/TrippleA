﻿using System.Collections;
using UnityEngine;

public class BossPhaseOne : MonoBehaviour, IDamageable {
    private Transform player;
    private Animator anim;

    [Header("HP")]
    [SerializeField]
    private int health = 10;

    [Header("Attack")]
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private Transform shootFromTransform;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int straightShotsAmount = 5;
    [SerializeField]
    private float timeBeetweenShotsAmount = 0.3f;
    [SerializeField]
    private float attacksPerMinute = 10;
    private float attackSpeed;
    private float attackSpeedHolder;

    [Header("On death")]
    [SerializeField]
    private GameObject nextBoss;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int numberOfExplosions = 25;
    [SerializeField]
    private float timeBetweenExplosions = 0.01f;
    private bool dead = false;

    private void Start() {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>().transform;
        if(attacksPerMinute > 0) {
            attackSpeed = 60 / attacksPerMinute;
        }
        attackSpeedHolder = Time.time + attackSpeed;
    }

    private void Update() {
        if(dead) {
            return;
        }

        if(Time.time >= attackSpeedHolder) {
            //anim.SetBool("Attacking", true);
            //float r = Random.Range(0.0f, 1.0f);
            //if(r <= 0.5) {
                StartCoroutine("AttackStraight");
            //} else {
               // StartCoroutine("AttackStrafe");
            //}
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

    private IEnumerator AttackStrafe() {
        return null;
    }

    public void TakeDamage(int takenDamage = 1) {
        if(dead) {
            return;
        }
        health -= takenDamage;
        //anim.SetTrigger("TookDamage");
        if(health <= 0) {
            Die();
        }
    }

    public void Die() {
        dead = true;
        StartCoroutine("SpawnExplosionEnum");
    }

    private IEnumerator SpawnInNextBoss() {
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(2.5f);
        if(nextBoss) {
            Instantiate(nextBoss, transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }

    private IEnumerator SpawnExplosionEnum() {
        anim.SetBool("Stop", true);
        for(int i = 0; i < numberOfExplosions; i++) {
            Vector2 point = Random.insideUnitCircle * 1.5f;
            Vector2 pos = new Vector3(transform.position.x + point.x, transform.position.y + point.y, transform.position.z);
            Instantiate(explosion, pos, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenExplosions);

        }
        StartCoroutine("SpawnInNextBoss");
        yield return null;
    }
}