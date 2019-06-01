using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : MonoBehaviour
{
    [SerializeField] EnemyHitBox myHitBox;

    Rigidbody2D rb;

    bool facingRight = true;

    const float moveForce = 11000f;
    const float maxSpeed = 30f;
    const float rayDistance = 70f;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myHitBox = Instantiate(myHitBox);
        myHitBox.SetEnemy(gameObject.transform, new Vector2(0f, 0f), new Vector2(1f, 1f));
    }

    private void Update()
    {
        if (rb.simulated && Mathf.Abs(rb.velocity.y) < Mathf.Epsilon)
        {
            Vector2 origin = transform.position;
            Vector2 direction = facingRight ? Vector2.right : Vector2.left;
            float distance = rayDistance;
            LayerMask collisionMask = 1 << LayerMask.NameToLayer("Block");
            RaycastHit2D hitWall = Physics2D.Raycast(origin, direction, distance, collisionMask);

            Vector2 forwardEdge = new Vector2(transform.position.x + (facingRight ? (rayDistance) : (-rayDistance)), transform.position.y);
            direction = Vector2.down;
            collisionMask = 1 << LayerMask.NameToLayer("Block");
            RaycastHit2D hitGround = Physics2D.Raycast(forwardEdge, direction, distance, collisionMask);
            distance = rayDistance;

            if (hitWall || !hitGround)
            {
                ChangeDirection();
            }
        }
    }

    private void FixedUpdate()
    {
        if (facingRight)
        {
            rb.AddForce(Vector2.right * moveForce);
        }
        else
        {
            rb.AddForce(Vector2.left * moveForce);
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    private void ChangeDirection()
    {
        facingRight = !facingRight;
    }
}
