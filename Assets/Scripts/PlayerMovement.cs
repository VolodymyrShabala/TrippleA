using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController2D controller;

    public float speed = 80f;
    float horizontalMove = 0f;
    bool jump = false;
    private Animator anim;
    private bool isWalking;
    private bool isJumping;
    private bool isRunning;
    private Rigidbody2D rb;
    private float dirX;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] int damage = 1;

    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Fire() {
        if(Input.GetButtonDown("Fire1")) {
            Projectile pr = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            pr.Damage = damage;
            pr.IgnoreCollision(gameObject);
        }
    }

    void Update() {
        Fire();

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if(Input.GetButtonDown("Jump") && rb.velocity.y == 0) {
            rb.AddForce(Vector2.up * 200f);
            jump = true;
        }

        if(Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);

        if(rb.velocity.y > 0)
            anim.SetBool("isJumping", true);
        if(rb.velocity.y <= 0)
            anim.SetBool("isJumping", false);

        if(Mathf.Abs(horizontalMove) == 0) {
            anim.SetBool("isWalking", false);
        } else if(Mathf.Abs(horizontalMove) <= 0.4) {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
        } else if(Mathf.Abs(horizontalMove) >= 0.4) {
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}