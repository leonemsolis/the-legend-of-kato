using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JumpState {GROUNDED, JUMPING, FALLING};

public class PlayerController : MonoBehaviour
{
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite leftSprite;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    JumpState jumpState = JumpState.FALLING;
    public static readonly float maxHoverDuration = 2f;
    float hoverTime = 0f;
    const float jumpForce = 2000f;
    const float hoverForce = 200f;


    bool facingRight = true;

    const float legDistance = 25f;
    const float minJumpAllowDistance = 60f;
    const float moveForce = 29200f;
    const float maxSpeed = 80f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
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

        Vector2 originLeft = new Vector2(transform.position.x - legDistance, transform.position.y - .5f);
        Vector2 originRight = new Vector2(transform.position.x + legDistance, transform.position.y - .5f);
        Vector2 direction = Vector2.down;
        LayerMask collisionMask = 1 << LayerMask.NameToLayer("Ground");

        RaycastHit2D leftHit = Physics2D.Raycast(originLeft, direction, minJumpAllowDistance, collisionMask);
        RaycastHit2D rightHit = Physics2D.Raycast(originRight, direction, minJumpAllowDistance, collisionMask);
        //Debug.DrawLine(originLeft, originLeft + direction * minJumpAllowDistance, Color.red);
        //Debug.DrawLine(originRight, originRight + direction * minJumpAllowDistance, Color.red);

        if(leftHit || rightHit)
        {
            jumpState = JumpState.GROUNDED;
            hoverTime = maxHoverDuration;
        }

        if(rb.velocity.y < -1f)
        {
            jumpState = JumpState.FALLING;
        }
    }

    private void MoveLeft()
    {
        if(facingRight)
        {
            facingRight = false;
            spriteRenderer.sprite = leftSprite;
        }
    }

    private void MoveRight()
    {
        if(!facingRight)
        {
            facingRight = true;
            spriteRenderer.sprite = rightSprite;
        }
    }

    public void ChangeDirection()
    {
        if (facingRight)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }
    }

    public void Jump()
    {
        if (jumpState == JumpState.GROUNDED)
        {
            jumpState = JumpState.JUMPING;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            if(jumpState == JumpState.FALLING)
            {
                if(hoverTime > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, hoverForce);
                    hoverTime -= Time.deltaTime;
                }
            }
        }
    }

    public float GetRemainHoverTime()
    {
        float oneSegment = maxHoverDuration / 4f;
        int segmented = (int)(Mathf.Ceil(hoverTime / oneSegment));
        return segmented / maxHoverDuration;
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }
}
