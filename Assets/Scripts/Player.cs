using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float speed;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            myRigidbody.velocity = new Vector2(-speed, myRigidbody.velocity.y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
        }
    }
}
