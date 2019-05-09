using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;

    float boxColliderAbsXOffset = 0.2490454f;
    Vector3 playerOffset;

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
        playerOffset = new Vector3(1f, 0f, 0f);
    }

    private void Update()
    {
        transform.position = player.transform.position + playerOffset;
        if(changeRequest == ChangeRequest.RIGHT)
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
        playerOffset = new Vector3(-1f, 0f, 0f);
        boxCollider.offset = new Vector2(boxColliderAbsXOffset, boxCollider.offset.y);
        spriteRenderer.sprite = null;
        changeRequest = ChangeRequest.LEFT;
    }

    public void TurnRight()
    {
        playerOffset = new Vector3(1f, 0f, 0f);
        boxCollider.offset = new Vector2(-boxColliderAbsXOffset, boxCollider.offset.y);
        spriteRenderer.sprite = null;
        changeRequest = ChangeRequest.RIGHT;
    }


}
