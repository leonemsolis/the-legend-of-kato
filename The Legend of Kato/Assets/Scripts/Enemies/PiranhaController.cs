using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PiranhaController : MonoBehaviour
{
    enum State { WALK, WALK_STRAIGHT, TP_OUT, TP_IN };
    [SerializeField] EnemyHitBox myHitBox;

    Rigidbody2D rb;
    PlayerController player;
    Animator animator;

    State state = State.WALK;

    const string TPOUT_ANIMATION = "Tp_out";
    const string TPIN_ANIMATION = "Tp_in";
    const string WALK_ANIMATION = "Walk";
    const float tpTime = .7f;
    float timer = 0;

    bool facingRight = true;
    const float tauntRange = 200f;
    const float tauntHeight = 400f;

    const float moveForce = 31000f;
    const float maxSpeed = 120f;

    const float rayDistance = 70f;

    bool usedTP = false;
    // Used after TP, Piranha will be in the air, so it'll start spinning
    // Timer used to disable spinning
    float walkStraightTime = 1f;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        myHitBox = Instantiate(myHitBox);
        myHitBox.SetEnemy(gameObject.transform, new Vector2(0f, -.03f), new Vector2(.8f, .93f), true);
    }

    void Update()
    {
        Vector2 origin = transform.position;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        float distance = rayDistance;
        LayerMask collisionMask = 1 << LayerMask.NameToLayer(C.BlockLayer);
        RaycastHit2D hitWall = Physics2D.Raycast(origin, direction, distance, collisionMask);

        Vector2 forwardEdge = new Vector2(transform.position.x + (facingRight ? (rayDistance) : (-rayDistance)), transform.position.y);
        direction = Vector2.down;
        collisionMask = 1 << LayerMask.NameToLayer(C.BlockLayer);
        RaycastHit2D hitGround = Physics2D.Raycast(forwardEdge, direction, distance, collisionMask);
        distance = rayDistance;

        switch(state)
        {
            case State.WALK:
                if (hitWall || !hitGround)
                {
                    ChangeDirection();
                }
                break;
            case State.WALK_STRAIGHT:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    state = State.WALK;
                }
                break;
            case State.TP_OUT:
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = tpTime;
                    state = State.TP_IN;
                    animator.Play(TPIN_ANIMATION);
                    Teleport();
                }
                break;
            case State.TP_IN:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    state = State.WALK_STRAIGHT;
                    timer = walkStraightTime;
                    animator.Play(WALK_ANIMATION);
                }
                break;
        }


        if (!usedTP)
        {
            if (SeeingPlayer())
            {
                StartTeleporting();
            }
        }
    }

    private void Teleport()
    {
        Vector3 newPosition;
        if (player.FacingRight)
        {
            newPosition = player.transform.position + new Vector3(-150f, +50f, 0f);
            if (!facingRight)
            {
                ChangeDirection();
            }
        }
        else
        {
            newPosition = player.transform.position + new Vector3(150f, +50f, 0f);
            if (facingRight)
            {
                ChangeDirection();
            }
        }
        transform.position = newPosition;
    }

    private void StartTeleporting()
    {
        usedTP = true;
        timer = tpTime;
        animator.Play(TPOUT_ANIMATION);
        state = State.TP_OUT;
    }

    private bool SeeingPlayer()
    {
        if (Mathf.Abs(transform.position.y - player.transform.position.y) < tauntHeight)
        {
            float d = Mathf.Abs(player.transform.position.x - transform.position.x);
            if (d < tauntRange && d >= 0)
            {
                return true;
            }
        }
        return false;
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case State.WALK:
            case State.WALK_STRAIGHT:
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
                break;
            case State.TP_IN:
            case State.TP_OUT:
                rb.velocity = new Vector2(0f, rb.velocity.y);
                break;
        }
    }

    private void ChangeDirection()
    {
        facingRight = !facingRight;

        // Default sprites turned right
        if (facingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
