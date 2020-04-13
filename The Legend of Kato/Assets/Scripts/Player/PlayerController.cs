#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JumpType { FIRST, SECOND, NONE };

public class PlayerController : MonoBehaviour 
{
    [SerializeField] AudioClip jumpSound;
    [SerializeField] GameObject movingDust;
    [SerializeField] GameObject jumpingDust;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    JumpType jumpType = JumpType.FIRST; 
    const float jumpForce = 1600f;

    bool facingRight = true;

    public bool FacingRight
    {
        get
        {
            return facingRight;
        }
    }

    bool canMove = true;
    bool grounded = false;

    public bool Grounded
    {
        get
        {
            return grounded;
        }
    }

    const float legDistance = 25f;
    const float minJumpAllowDistance = 60f;
    const float moveForce = 29200f;
    const float maxSpeed = 80f;

    public bool CanMove { get => canMove; set => canMove = value; }

    private void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate ()
    {
        grounded = Mathf.Abs(rb.velocity.y) < Mathf.Epsilon;
        if (CanMove)
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
            LayerMask collisionMask = 1 << LayerMask.NameToLayer("Block");

            RaycastHit2D leftHit = Physics2D.Raycast(originLeft, direction, minJumpAllowDistance, collisionMask);
            RaycastHit2D rightHit = Physics2D.Raycast(originRight, direction, minJumpAllowDistance, collisionMask);
            //Debug.DrawLine(originLeft, originLeft + direction * minJumpAllowDistance, Color.red);
            //Debug.DrawLine(originRight, originRight + direction * minJumpAllowDistance, Color.red);

            if ((leftHit || rightHit) && rb.velocity.y <= 0)
            {
                jumpType = JumpType.FIRST;
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

	private void MoveLeft ()
	{
		if (facingRight) {
			facingRight = false;
            //rb.AddForce(Vector2.left * moveForce * 2f);
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
	}

	private void MoveRight ()
	{
		if (!facingRight) {
			facingRight = true;
            //rb.AddForce(Vector2.right * moveForce * 2f);
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
	}
    

	public void ChangeDirection ()
	{
        if(CanMove)
        {
            if (facingRight)
            {
                MoveLeft();
                Instantiate(movingDust, transform.position, Quaternion.identity).transform.localScale = new Vector3(.7f, .7f, .7f);
            }
            else
            {
                MoveRight();
                Instantiate(movingDust, transform.position, Quaternion.identity).transform.localScale = new Vector3(-.7f, .7f, .7f);
            }
        }
	}

	public void Jump ()
	{
        if(CanMove)
        {
            switch (jumpType)
            {
                case JumpType.SECOND:
                    FindObjectOfType<SoundPlayer>().PlaySound(jumpSound, transform.position);
                    Instantiate(jumpingDust, transform.position, Quaternion.identity).transform.localScale = new Vector3(facingRight ? 1f : -1f, 1f, 1f);
                    jumpType = JumpType.NONE;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    break;
                case JumpType.FIRST:
                    FindObjectOfType<SoundPlayer>().PlaySound(jumpSound, transform.position);
                    Instantiate(jumpingDust, transform.position, Quaternion.identity).transform.localScale = new Vector3(facingRight ? 1f : -1f, 1f, 1f);
                    jumpType = JumpType.SECOND;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    break;
                case JumpType.NONE:
                    break;
            }
        }
	}

    public void Push(float pushForce)
    {
        rb.AddForce(new Vector2(0f, pushForce), ForceMode2D.Impulse);
    }
}