using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Sprite rightSprite;
    [SerializeField] Sprite leftSprite;
    SwordController sword;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    // TODO: change color switching of the buttons in the button scripts
    SpriteRenderer leftButtonSpriteRenderer;
    SpriteRenderer rightButtonSpriteRenderer;
    SpriteRenderer activeButtonSpriteRenderer;


    const float legDistance = .25f;
    bool facingRight = true;
    bool jump = false;
    float moveForce = 365f;
    float maxSpeed = 1f;
    float jumpForce = 1500f;
    bool grounded = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sword = FindObjectOfType<SwordController>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        leftButtonSpriteRenderer = FindObjectOfType<ButtonLeft>().GetComponent<SpriteRenderer>();
        rightButtonSpriteRenderer = FindObjectOfType<ButtonRight>().GetComponent<SpriteRenderer>();
        activeButtonSpriteRenderer = FindObjectOfType<ButtonActive>().GetComponent<SpriteRenderer>();
        //Time.timeScale = .1f;
    }


    private void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeDirection();
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        Vector2 originLeft = new Vector2(transform.position.x - legDistance, transform.position.y);
        Vector2 originRight = new Vector2(transform.position.x + legDistance, transform.position.y);
        Vector2 direction = Vector2.down;
        float distance = .6f;
        LayerMask collisionMask = 1<<LayerMask.NameToLayer("Ground");

        RaycastHit2D leftHit = Physics2D.Raycast(originLeft, direction, distance, collisionMask);
        RaycastHit2D rightHit = Physics2D.Raycast(originRight, direction, distance, collisionMask);

        grounded = leftHit || rightHit;
        Debug.DrawLine(originLeft, originLeft + (direction * distance), Color.red);
        Debug.DrawLine(originRight, originRight + (direction * distance), Color.red);
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

        if(jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }

    public void MoveLeft()
    {
        if(facingRight)
        {
            leftButtonSpriteRenderer.color = Color.gray;
            rightButtonSpriteRenderer.color = Color.white;
            facingRight = false;
            spriteRenderer.sprite = leftSprite;
            sword.TurnLeft();
        }
    }

    public void MoveRight()
    {
        if(!facingRight)
        {
            rightButtonSpriteRenderer.color = Color.gray;
            leftButtonSpriteRenderer.color = Color.white;
            facingRight = true;
            spriteRenderer.sprite = rightSprite;
            sword.TurnRight();
        }
    }

    public void ChangeDirection()
    {
        if(facingRight)
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
        if(grounded)
        {
            activeButtonSpriteRenderer.color = Color.gray;
            StartCoroutine(ChangeActiveButtonColor());
            jump = true;
        }
    }

    private IEnumerator ChangeActiveButtonColor()
    {
        yield return new WaitForSeconds(.1f);
        activeButtonSpriteRenderer.color = Color.white;
    }

    public void TakeDamage()
    {
        FindObjectOfType<Health>().TakeDamage();
    }
}
