using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public float moveSpeed = 10f;
    private bool faceRight = true;
    public float moveX;
    
    public float jumpVelocity = 10f;

    public LayerMask groundLayer;

    Rigidbody2D rb;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
	}
	
    
	// Update is called once per frame
	void Update () {
        moveX = Input.GetAxis("Horizontal");
        if(moveX > 0.0f)
        {
            Flip();
        }
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        // make the jump faster when player is falling
        
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = Vector2.up * jumpVelocity;
        }
        
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

    }
}
