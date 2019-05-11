﻿using System.Collections;
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
        //Time.timeScale = .1f;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeDirection();
            FindObjectOfType<ButtonMove>().Blink();
        }
        if (Input.GetKeyDown(KeyCode.L) && grounded)
        {
            Jump();
            FindObjectOfType<ButtonActive>().Blink();
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

        if(jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }



        Vector2 originLeft = new Vector2(transform.position.x - legDistance, transform.position.y);
        Vector2 originRight = new Vector2(transform.position.x + legDistance, transform.position.y);
        Vector2 direction = Vector2.down;
        float distance = .6f;
        LayerMask collisionMask = 1 << LayerMask.NameToLayer("Ground");

        RaycastHit2D leftHit = Physics2D.Raycast(originLeft, direction, distance, collisionMask);
        RaycastHit2D rightHit = Physics2D.Raycast(originRight, direction, distance, collisionMask);

        grounded = leftHit || rightHit;
    }

    private void MoveLeft()
    {
        if(facingRight)
        {
            facingRight = false;
            spriteRenderer.sprite = leftSprite;
            sword.TurnLeft();
        }
    }

    private void MoveRight()
    {
        if(!facingRight)
        {
            facingRight = true;
            spriteRenderer.sprite = rightSprite;
            sword.TurnRight();
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
        if(grounded)
        {
            jump = true;
        }
    }

    private IEnumerator ResetButtonColor(SpriteRenderer buttonSpriteRenderer)
    {
        yield return new WaitForSeconds(.1f);
        buttonSpriteRenderer.color = Color.black;
    }

    public void TakeDamage()
    {
        //FindObjectOfType<Health>().TakeDamage();
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }
}
