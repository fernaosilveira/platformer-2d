using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public string groundTag = "Ground";

    [Header("Movement Params")]
    public float speed;
    public float speedRun;
    public float jumpForce = 1f;
    public Vector2 friction = new Vector2(.1f, 0);

    private float _currentSpeed;
    private bool _isGrounded;

    void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (_isGrounded == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed = speedRun;
            }
            else
            {
                _currentSpeed = speed;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
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
        if (_isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.velocity = Vector2.up * jumpForce;
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            _isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag)
        {
            _isGrounded = true;
        }
    }

}
