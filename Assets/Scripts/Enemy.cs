using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    // Use this for initialization
    public float moveSpeed = 4f;
    private float transparency = 1f;
    private bool faceRight;
    private bool faceLeft;
    private bool attacking;
    private bool colliding = false;
    private bool dying = false;
    private int hp = 30;

    public LayerMask groundLayer;
    public LayerMask growLayer;
    public GameObject Coin;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        faceRight = true;
        faceLeft = false;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        DamageTree();
        HandleHorizontalMovement();
        CheckHP();
        HandleAnimations();
        Debug.Log(hp);
    }

    void DamageTree() { }

    void CheckHP()
    {
        if (hp <= 0)
        {
            dying = true;
        }
    }



    public void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.collider.gameObject.tag == "Blade")
        {
            hp -= 10;
        }
    }

    private void HandleAnimations()
    {

        if (attacking && !dying)
        {
            animator.SetBool("attacking", true);
        }

        if (dying)
        {
            Debug.Log("Dying");
            animator.SetBool("dead", true);
            if (transparency > 0)
            {
                transparency -= 0.02f;
                sr.color = new Color(1f, 1f, 1f, transparency);
            }
            else
            {
                Instantiate(Coin, transform.position, transform.rotation);
                GameObject.Destroy(this.gameObject);
            }
        }
    }


    private void HandleHorizontalMovement()
    {
        if (!attacking)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    private void Flip()

    {
        faceRight = !faceRight;
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
