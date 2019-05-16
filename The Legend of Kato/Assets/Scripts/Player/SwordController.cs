using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;

    const float boxColliderRightXOffset = -0.2518227f;
    const float boxColliderLeftXOffset = 0.2518227f;
    Vector3 rightPosition = new Vector3(1f, 0f, 0f);
    Vector3 leftPosition = new Vector3(-1f, 0f, 0f);

    PlayerController player;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;
    enum ChangeRequest {LEFT, RIGHT, DONE};
    ChangeRequest changeRequest = ChangeRequest.DONE;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (changeRequest == ChangeRequest.RIGHT)
        {
            changeRequest = ChangeRequest.DONE;
            spriteRenderer.sprite = rightSprite;
        } else if(changeRequest == ChangeRequest.LEFT)
        {
            changeRequest = ChangeRequest.DONE;
            spriteRenderer.sprite = leftSprite;
        }
    }

    public void TurnLeft()
    {
        spriteRenderer.sprite = null;
        boxCollider.enabled = false;
        transform.localPosition = leftPosition;
        boxCollider.offset = new Vector2(boxColliderLeftXOffset, boxCollider.offset.y);
        changeRequest = ChangeRequest.LEFT;
        boxCollider.enabled = true;
    }

    public void TurnRight()
    {
        spriteRenderer.sprite = null;
        boxCollider.enabled = false;
        transform.localPosition = rightPosition;
        boxCollider.offset = new Vector2(boxColliderRightXOffset, boxCollider.offset.y);
        changeRequest = ChangeRequest.RIGHT;
        boxCollider.enabled = true;
    }
}
