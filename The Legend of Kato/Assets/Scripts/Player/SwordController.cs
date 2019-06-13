#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    //bool facingRight = true;

    //Vector3 rightPosition = new Vector3(100f, 0f, 0f);
    //Vector3 leftPosition = new Vector3(-100f, 0f, 0f);
    //Vector2 colliderRightOffset = new Vector2(-18.90197f, -12.52582f);
    //Vector2 colliderLeftOffset = new Vector2(18.90197f, -12.52582f);

    PlayerController player;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //if (player.FacingRight && !facingRight)
        //{
        //    TurnRight();
        //}
        //if (!player.FacingRight && facingRight)
        //{
        //    TurnLeft();
        //}
    }

    public void TurnLeft()
    {
        spriteRenderer.sprite = null;
        boxCollider.enabled = false;
        //transform.localPosition = leftPosition;
        //boxCollider.offset = colliderLeftOffset;
        boxCollider.enabled = true;
        //facingRight = false;
    }

    public void TurnRight()
    {
        //spriteRenderer.sprite = null;
        //boxCollider.enabled = false;
        //transform.localPosition = rightPosition;
        //boxCollider.offset = colliderRightOffset;
        //boxCollider.enabled = true;
        //facingRight = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyHitBox")
        {
            collision.GetComponent<EnemyHitBox>().Die(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "EnemyHitBox")
        {
            collision.GetComponent<EnemyHitBox>().Die(true);
        }
    }
}
