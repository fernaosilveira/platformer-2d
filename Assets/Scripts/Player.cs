using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float jumpForce = 1f;
    public Vector2 dash = new Vector2(5, 0);

    void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
        }

        if(myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity -= friction;
        }

        else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = Vector2.up * jumpForce; 
        }
    }
}
