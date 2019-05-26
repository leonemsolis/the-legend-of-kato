#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;

    bool facingRight = true;

    Vector3 rightPosition = new Vector3(100f, 0f, 0f);
    Vector3 leftPosition = new Vector3(-100f, 0f, 0f);
    Vector3 bottomPosition = new Vector3(0f, -100f, 0f);
    Vector2 colliderRightOffset = new Vector2(-25.18227f, -15.7839f);
    Vector2 colliderLeftOffset = new Vector2(25.18227f, -15.7839f);
    Vector2 colliderBottomOffset = new Vector2(0f, 30f);

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
        if (player.IsFacingRight() && !facingRight)
        {
            TurnRight();
        }
        if (!player.IsFacingRight() && facingRight)
        {
            TurnLeft();
        }
    }

    public void TurnLeft()
    {
        spriteRenderer.sprite = null;
        boxCollider.enabled = false;
        transform.localPosition = leftPosition;
        boxCollider.offset = colliderLeftOffset;
        boxCollider.enabled = true;
        facingRight = false;
        spriteRenderer.sprite = leftSprite;
    }

    public void TurnRight()
    {
        spriteRenderer.sprite = null;
        boxCollider.enabled = false;
        transform.localPosition = rightPosition;
        boxCollider.offset = colliderRightOffset;
        boxCollider.enabled = true;
        facingRight = true;
        spriteRenderer.sprite = rightSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyHitBox")
        {
            collision.GetComponent<EnemyHitBox>().Die();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "EnemyHitBox")
        {
            collision.GetComponent<EnemyHitBox>().Die();
        }
    }
}
