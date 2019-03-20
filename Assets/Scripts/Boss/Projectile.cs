using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private int damage = 1;
    private Vector3 dir;

    [SerializeField]
    private float moveSpeed = 10.0f;
    private float moveThisFrame;

    private bool collided = false;
    private float lifeTime = 0;

    private GameObject toIgnore;

    private void FixedUpdate() {
        if(collided) {
            return;
        }
        lifeTime += Time.fixedDeltaTime;
        if(lifeTime >= 30.0f) {
            Destroy(gameObject);
        }
        moveThisFrame = moveSpeed * Time.deltaTime;
        CheckCollisionThisFrame();
        transform.Translate(dir * moveThisFrame);
    }

    private void CheckCollisionThisFrame() {
        Debug.DrawRay(transform.position, dir * moveThisFrame);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, moveThisFrame);
        if(hit.collider == null || hit.collider.gameObject == toIgnore) {
            return;
        }
        if(hit.collider.GetComponent<IDamageable>() != null) {
            hit.collider.GetComponent<IDamageable>().TakeDamage(damage);
        }
        collided = true;
        StartCoroutine("Collided");
        Debug.Log("Hit: " + hit.collider.name);
    }

    private IEnumerator Collided() {
        GetComponent<Animator>().SetBool("Explode", true);
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
        yield return null;
    }

    public void MoveTo(Vector3 playerPosition) {
        dir = (playerPosition - transform.position).normalized;
    }

    public void IgnoreCollision(GameObject go) {
        toIgnore = go;
    }

    public int Damage { set { damage = value; } }
}