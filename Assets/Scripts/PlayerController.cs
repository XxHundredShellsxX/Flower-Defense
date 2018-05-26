using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public float moveSpeed = 10f;
    private bool faceRight = true;
    private bool attack;
    
    public float jumpVelocity = 10f;

    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
    
	// Update is called once per frame
	void Update () {
        bool moveRight = Input.GetKey("right");
        bool moveLeft = Input.GetKey("left");
        bool attacking = Input.GetKey("z");

        if(moveRight && !moveLeft)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            if (!faceRight)
            {
                Flip();
            }
        }
        else if (moveLeft && !moveRight)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            if (faceRight)
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
        if (attacking)
        {
            attack = true;
        }
        HandleAttacks();

        
    }


    bool isGrounded()
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

    void Flip()

    {
        faceRight = !faceRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void HandleAttacks()
    {
        if (attack)
        {
            animator.SetTrigger("attack");
        }
        attack = false;
    }
}
