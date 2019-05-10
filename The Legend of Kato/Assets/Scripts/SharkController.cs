﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;
    [SerializeField] EnemyHitBox myHitBox;

    Rigidbody2D rb;
    PlayerController player;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    bool facingRight = true;
    float moveForce = 182f;
    float maxSpeed = .5f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myHitBox = Instantiate(myHitBox);
        myHitBox.SetEnemy(gameObject.transform);
        myHitBox.SetFacingRight(facingRight);
    }

    private void Update()
    {
        Vector2 origin = transform.position;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        float distance = .6f;
        LayerMask collisionMask = 1 << LayerMask.NameToLayer("Wall");
        RaycastHit2D hitWall = Physics2D.Raycast(origin, direction, distance, collisionMask);
        //Debug.DrawLine(origin, origin + direction * distance, Color.green);

        Vector2 forwardEdge = new Vector2(transform.position.x + (facingRight ? (.7f) : (-.7f)), transform.position.y);
        direction = Vector2.down;
        collisionMask = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hitGround = Physics2D.Raycast(forwardEdge, direction, distance, collisionMask);
        distance = .7f;
        //Debug.DrawLine(forwardEdge, forwardEdge + direction * distance, Color.red);

        if (hitWall || !hitGround)
        {
            ChangeDirection();
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
        spriteRenderer.sprite = facingRight ? rightSprite : leftSprite;
        myHitBox.SetFacingRight(facingRight);
    }
}
