using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float speed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    private Animator anim;
    private bool isJumping;
    private bool isRunning;
    private Rigidbody2D rb;
    private float dirX;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 20f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }

    void Update()
    {
        Fire();

        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump") && rb.velocity.y == 0)
            rb.AddForce(Vector2.up * 200f);

        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
           
        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            

        }
        {
            if (rb.velocity.y > 0)
                anim.SetBool("isJumping", true);
            if (rb.velocity.y < 0)
                anim.SetBool("isJumping", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
           
        }

       


        if (horizontalMove == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;


    }

}