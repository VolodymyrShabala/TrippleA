using System.Collections;
using UnityEngine;

public class BossPhaseOneProjectile : MonoBehaviour {
    private int damage = 1;
    private Vector3 dir;

    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private LayerMask playerLayer;

    private bool collided = false;
    private float lifeTime = 0;

    private void FixedUpdate() {
        if(collided) {
            return;
        }
        lifeTime += 0.2f;
        if(lifeTime >= 20.0f) {
            Destroy(gameObject);
        }
        CheckCollisionThisFrame();
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void CheckCollisionThisFrame() {
        Debug.DrawRay(transform.position, dir * moveSpeed * Time.deltaTime, Color.red, 0.1f);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir * moveSpeed * Time.deltaTime, out hit)) {
            //hit.collider.GetComponent<IDamageable>().TakeDamage(damage);
            //StartCoroutine("Collided");
            print("Hit: " + hit.collider.name);
        }
    }

    private IEnumerator Collided() {
        collided = true;
        GetComponent<Animator>().SetBool("Explode", true);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
        yield return null;
    }

    public void MoveTo(Vector3 playerPosition) {
        dir = playerPosition - transform.position;
    }

    public int Damage { set { damage = value; } }
}