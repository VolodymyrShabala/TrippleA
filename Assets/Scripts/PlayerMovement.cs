using UnityEngine;

public class PlayerMovement : MonoBehaviour,/*IDamageable*/ {

    public CharacterController2D controller;

    [SerializeField] public float speed = 80f;
    float horizontalMove = 0f;

    private Animator anim;
    public bool jump = false;
    private Rigidbody2D rb;


    [SerializeField] Projectile projectilePrefab;
    [SerializeField] int damage = 1;

    private void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
            rb.AddForce(Vector2.up * 200f);
            jump = true;
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("isShooting");
            Projectile pr = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            pr.Damage = damage;
            pr.IgnoreCollision(gameObject);
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * speed * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    //public void TakeDamage(int damage = 1)
    //{
    //    throw new System.NotImplementedException();
    //}

    //public void Die()
    //{
    //    throw new System.NotImplementedException();
    //}
}




//if (rb.velocity.y == 0)
//    anim.SetBool("isWalking", true);
//else 
//    anim.SetBool("isWalking", false);

//if (rb.velocity.y > 0)
//    anim.SetBool("isJumping", true);

//if (rb.velocity.y <= 0)
//    anim.SetBool("isJumping", false);

//if (Mathf.Abs(horizontalMove) == 0)
//{
//    anim.SetBool("isWalking", false);
//}
//else if (Mathf.Abs(horizontalMove) <= 0.4)
//{
//    anim.SetBool("isRunning", false);
//    anim.SetBool("isWalking", true);
//}
//else if (Mathf.Abs(horizontalMove) >= 0.4)
//{
//    anim.SetBool("isRunning", true);
//    anim.SetBool("isWalking", false);
//}