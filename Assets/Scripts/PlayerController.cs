using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public float moveSpeed = 10f;
    private bool faceRight = true;
    private bool attack;
    private bool canAttack;
    private bool grounded;
    private bool jumping;
    bool moveRight;
    bool moveLeft;
    bool attacking;



    public float jumpVelocity = 10f;

    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    public EdgeCollider2D MeleeCollider2;


    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
	
    
	// Update is called once per frame
	void FixedUpdate () {
        moveRight = Input.GetKey("right");
        moveLeft = Input.GetKey("left");
        attacking = Input.GetKey("z");
        jumping = Input.GetButtonDown("Jump");

        grounded = isGrounded();

        HandleHorizontalMovement();
        if (jumping && grounded)
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
        // player cant attack again until they let go of attack button
        if (!attacking)
        {
            canAttack = true;
        }
        if (attacking && canAttack)
        {
            attack = true;
        }
        HandleAnimations();
        resetValues();


        
    }

    private void HandleAnimations()
    {
        Debug.Log(jumping);
        
        if (grounded)
        {
            animator.SetBool("jumping", false);
            animator.SetFloat("speed", Math.Abs(rb.velocity.x));
            if (attack)
            {
                animator.SetTrigger("attack");
                canAttack = false;
            }
        }
        else
        {
            animator.SetBool("jumping", true);
            if (attack)
            {
                animator.SetTrigger("jumpAttack");
                canAttack = false;
            }
        }
    }


    private void HandleHorizontalMovement()
    {
        if (moveRight && !moveLeft)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            // Flip sprite if other direction
            if (!faceRight)
            {
                Flip();
            }
        }
        else if (moveLeft && !moveRight)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            // Flip sprite if faving new direction
            if (faceRight)
            {
                Flip();
            }
        }
        // when not going left or right it stops moving in the x direction
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void resetValues()
    {
        attack = false;
    }
    private bool isGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float dist = 1.0f;
        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, dist, groundLayer);
        if(hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private void Flip()

    {
        faceRight = !faceRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }


    public void setMeleeCollider()
    {
        MeleeCollider2.enabled = !MeleeCollider2.enabled;
    }
}
