using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordfishController : Respawnable
{

    [SerializeField] EnemyHitBox hitBoxPrefab;
    [SerializeField] EnemyHitBox swordHitBoxPrefab;
    EnemyHitBox swordHitBox;
    EnemyHitBox hitBox;
    Rigidbody2D rb;
    Animator animator;

    bool facingRight = false;

    const float moveForce = 40000f;
    const float maxSpeed = 100f;

    const float speedDeceleration = moveForce / 2f;
    float currentAcceleration = moveForce;

    const float rayDistance = 110f;

    const string ChargeAnimationName = "Charge";
    const float chargeAnimationTime = .5f;
    bool waitForCharge = true;
    float chargeAnimationTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitBox = Instantiate(hitBoxPrefab);
        hitBox.SetEnemy(gameObject.transform, new Vector2(.75f, 0f), new Vector2(.5f, 1f), true);
         
        swordHitBox = Instantiate(swordHitBoxPrefab);
        swordHitBox.SetEnemy(gameObject.transform, new Vector2(-.25f, 0f), new Vector2(1.5f, 1f), false);
    }


    void Update()
    {
        if(running)
        {
            Vector2 origin = transform.position;
            Vector2 direction = facingRight ? Vector2.right : Vector2.left;
            float distance = rayDistance;
            LayerMask collisionMask = 1 << LayerMask.NameToLayer(C.BlockLayer);
            RaycastHit2D hitWall = Physics2D.Raycast(origin, direction, distance, collisionMask);

            if (hitWall)
            {
                ChangeDirection();
            }
        }
        else
        {
            swordHitBox.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        if(swordHitBox != null)
        {
            Destroy(swordHitBox.transform.gameObject);
        }
    }

    public override void Respawn()
    {
        base.Respawn();
        swordHitBox.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(facingRight ? -1f : 1f, 1f, 1f);
        currentAcceleration = moveForce;
        waitForCharge = true;
        chargeAnimationTimer = 0f;
        animator.Play(ChargeAnimationName);
        hitBox.ChangeBox(new Vector2(facingRight ? -.75f : .75f, 0f), new Vector2(.5f, 1f));
        swordHitBox.ChangeBox(new Vector2(facingRight ? .25f : -.25f, 0f), new Vector2(1.5f, 1f));
    }

    private void FixedUpdate()
    {
        if(running)
        {
            if (!waitForCharge)
            {
                if (facingRight)
                {
                    rb.AddForce(Vector2.right * currentAcceleration);
                }
                else
                {
                    rb.AddForce(Vector2.left * currentAcceleration);
                }
                if (Mathf.Abs(rb.velocity.x) > maxSpeed)
                {
                    rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
                }
                if (currentAcceleration > speedDeceleration)
                {
                    currentAcceleration -= speedDeceleration * Time.deltaTime;
                }
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                if (chargeAnimationTimer > chargeAnimationTime)
                {
                    waitForCharge = false;
                }
                else
                {
                    chargeAnimationTimer += Time.deltaTime;
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
}
