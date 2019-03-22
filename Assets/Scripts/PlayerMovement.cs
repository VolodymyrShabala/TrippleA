using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour, IDamageable {
    private CharacterController2D controller;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private AudioSource shootSFX;
    [SerializeField] private AudioSource jumpSFX;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform placeToShootFrom;
    [SerializeField] int damage = 1;

    [SerializeField] private float speed = 80f;
    float horizontalMove = 0f;
    private bool jump = false;

    private int health = 3;

    private void Start() {
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Fire();
        Move();
    }

    private void Move() {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("VelocityX", (Mathf.Abs(horizontalMove)));
        anim.SetFloat("VelocityY", rb.velocity.y);

        if(Input.GetButtonDown("Jump") && rb.velocity.y == 0) {
            jumpSFX.Play();
            rb.AddForce(Vector2.up * 200f);
            jump = true;
        }
    }

    private void Fire() {
        if(Input.GetButtonDown("Fire1")) {
            shootSFX.Play();
            anim.SetTrigger("isShooting");
            Projectile pr = Instantiate(projectilePrefab, placeToShootFrom.position, Quaternion.identity);
            pr.Damage = damage;
            pr.IgnoreCollision(gameObject);
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * speed * Time.fixedDeltaTime, jump);
        jump = false;
    }

    public void TakeDamage(int damage = 1) {
        health = health - damage;
        CameraShake.ShakeFor(0.3f);
        if(health <= 0) {
            Die();
        }
    }

    public void Die()
    {
        FindObjectOfType<GameManager>().GameOver();
    }
}