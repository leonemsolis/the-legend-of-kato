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
    float startX = 0;
    const float maxDeltaX = 1200f;

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
        if(Mathf.Abs(transform.position.x - startX) > maxDeltaX)
        {
            Destroy(gameObject);
        }
    }

    public void StartMoving(bool movingRight)
    {
        startX = transform.position.x;
        GetComponent<Animator>().Play("Projectile_open");
        // Default sprites turned left
        if (movingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        rb = GetComponent<Rigidbody2D>();
        direction = movingRight ? Vector2.right : Vector2.left;
        moving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == C.BlockTag)
        {
            Destroy(gameObject);
        }
    }
}
