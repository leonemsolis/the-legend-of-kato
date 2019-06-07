#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    [SerializeField] EnemyHitBox myHitBox;

    Rigidbody2D rb;
    PlayerController player;
    Animator animator;

    const float minChangeDirTime = .4f;
    const float maxChangeDirTime = 1f;
    float changeDirTimer = maxChangeDirTime;

    bool facingRight = true;
    const float tauntRange = 500f;
    const float tauntHeight = 100f;

    const float passiveMoveForce = 11000f;
    const float passiveMaxSpeed = 30f;

    const float activeMoveForce = 29200f;
    const float activeMaxSpeed = 80f;

    float moveForce = passiveMoveForce;
    float maxSpeed = passiveMaxSpeed;

    const float rayDistance = 70f;


    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        myHitBox = Instantiate(myHitBox);
        myHitBox.SetEnemy(gameObject.transform, new Vector2(0f, -.03f), new Vector2(1f, .93f), true);
        animator = GetComponent<Animator>();

        if(Random.Range(0f, 1f) > .5f)
        {
            ChangeDirection();
        }
    }

    private void Update()
    {
        if(rb.simulated && Mathf.Abs(rb.velocity.y) < Mathf.Epsilon)
        {
            TickTimer();

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

            // boost speed when shark sees player

            if(SeeingPlayer())
            {
                moveForce = activeMoveForce;
                maxSpeed = activeMaxSpeed;
                // Update timer each frame shark see player, in order to chase in current direction
                changeDirTimer = minChangeDirTime;
            }
            else
            {
                moveForce = passiveMoveForce;
                maxSpeed = passiveMaxSpeed;
            }
        }
    }

    private bool SeeingPlayer()
    {
        if(Mathf.Abs(transform.position.y - player.transform.position.y) < tauntHeight)
        {
            if(facingRight)
            {
                float d = player.transform.position.x - transform.position.x;
                if (d < tauntRange && d >= 0) 
                {
                    return true;
                }
            }
            else
            {
                float d = transform.position.x - player.transform.position.x;
                if (d < tauntRange && d >= 0)
                {
                    return true;
                }
            }
        }
        return false;
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

    private void TickTimer()
    {
        changeDirTimer -= Time.deltaTime;
        if(changeDirTimer < 0)
        {
            ChangeDirection();
            RestartTimer();
        }
    }

    private void RestartTimer()
    {
        changeDirTimer = Random.Range(minChangeDirTime, maxChangeDirTime);
    }

    private void ChangeDirection()
    {
        facingRight = !facingRight;
        animator.SetBool("facing_right", facingRight);
    }
}
