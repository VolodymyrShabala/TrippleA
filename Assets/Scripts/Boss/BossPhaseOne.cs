using System.Collections;
using UnityEngine;

public class BossPhaseOne : Boss {
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform shootFromTransform;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private int straightShotsAmount = 1;
    [SerializeField]
    private float timeBeetweenShotsAmount = 0.3f;
    [SerializeField]
    private float attacksPerSecond = 10;
    private float attackSpeed;
    private float attackSpeedHolder;

    private void Start() {
        if(attacksPerSecond > 0) {
            attackSpeed = 60 / attacksPerSecond;
        }
    }

    private void Update() {
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
            GameObject go = Instantiate(projectile, shootFromTransform.position, Quaternion.identity);
            BossPhaseOneProjectile pr = go.GetComponent<BossPhaseOneProjectile>();
            pr.MoveTo(player.position);
            pr.Damage = damage;
            yield return new WaitForSeconds(timeBeetweenShotsAmount);
        }
        yield return null;
    }

    private IEnumerator AttackStrafe() {
        return null;
    }
}