using UnityEngine;

public class PlayerMovement : MonoBehaviour,IDamageable {

    public CharacterController2D controller;

    [SerializeField] public float speed = 80f;
    float horizontalMove = 0f;

    private Animator anim;
    public bool jump = false;
    private Rigidbody2D rb;
    private int health = 3;
    private bool dead = false;
    [SerializeField] Transform placeToShootFrom;

    private AudioSource audioSource;

    public AudioSource jumpSFX;


    [SerializeField] Projectile projectilePrefab;
    [SerializeField] int damage = 1;

    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        Fire();
        Move();
    }

    private void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("VelocityX", (Mathf.Abs(horizontalMove)));
        anim.SetFloat("VelocityY", rb.velocity.y);

        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
        {
            jumpSFX.Play();
            rb.AddForce(Vector2.up * 200f);
            jump = true;
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
            
        {
            audioSource.Play();
            anim.SetTrigger("isShooting");
            Projectile pr = Instantiate(projectilePrefab, placeToShootFrom.position, Quaternion.identity);
            pr.Damage = damage;
            pr.IgnoreCollision(gameObject);
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * speed * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void TakeDamage(int damage = 1)
    {
        health = health - damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Tetris")
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}