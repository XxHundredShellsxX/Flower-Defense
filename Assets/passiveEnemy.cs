using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PassiveEnemy : MonoBehaviour
{

    // Use this for initialization
    public float moveSpeed = 4f;
    private float transparency = 1f;
    private bool faceRight;
    private bool faceLeft;
    private bool dying = false;
    private bool notMoving = true;
    private int hp = 20;
    private float timer = 0;

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
        //transform.position = new Vector3(UnityEngine.Random.value,UnityEngine.Random.value,0f);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        HandleHorizontalMovement();
        CheckHP();
        HandleAnimations();
    }

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
        if (notMoving) {
            timer = 360 * UnityEngine.Random.value - 180;
            if (timer < 0)
            {
                if (faceRight)
                {
                    Flip();
                }
                faceLeft = true;
                faceRight = false;
                timer *= -1;
            }
            else
            {
                if (faceLeft)
                {
                    Flip();
                }
                faceLeft = false;
                faceRight = true;
            }
            notMoving = false;
        }

        else
        {
            if (timer > 0)
            {
                if (faceRight)
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                }
                timer--;
            }
            else
            {
                notMoving = true;
            }
        }
    }

    private void Flip()

    {
        Vector2 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
