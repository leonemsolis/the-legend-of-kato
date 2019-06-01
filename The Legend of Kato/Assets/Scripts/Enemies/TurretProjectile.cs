using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    Rigidbody2D rb;

    const float moveForce = 29200f;
    const float maxSpeed = 80f;
    bool moving = false;
    Vector2 direction = Vector2.zero;

    void Update()
    {
        if(moving)
        {
            rb.AddForce(direction * moveForce);
            if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            {
                rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
            }
        }
    }

    public void StartMoving(bool movingRight)
    {
        rb = GetComponent<Rigidbody2D>();
        direction = movingRight ? Vector2.right : Vector2.left;
        GetComponent<Animator>().SetBool("moving_right", movingRight);
        moving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Block")
        {
            Destroy(gameObject);
        }
    }
}
