using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsLock : MonoBehaviour
{
    enum State {OPENED, UNLOCKED, LOCKED};

    [SerializeField] int LockID;

    [SerializeField] Sprite locked;
    [SerializeField] Sprite unlocked;
    [SerializeField] Sprite buttonUp;

    SpriteRenderer spriteRender;
    State state;

    CircleCollider2D circleCollider;
    BoxCollider2D boxCollider;
    ButtonUI buttonUI;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        buttonUI = GetComponent<ButtonUI>();

        spriteRender = GetComponent<SpriteRenderer>();

        switch(LockID)
        {
            case 1:
                state = State.OPENED;
                break;
            case 2:
                state = State.UNLOCKED;
                break;
            case 3:
                state = State.LOCKED;
                break;
        }

        SetLocker();
    }

    void SetLocker()
    {
        switch (state)
        {
            case State.OPENED:
                spriteRender.sprite = buttonUp;
                circleCollider.enabled = false;
                boxCollider.enabled = true;
                buttonUI.enabled = true;
                break;
            case State.UNLOCKED:
                spriteRender.sprite = locked;
                buttonUI.enabled = false;
                boxCollider.enabled = false;
                break;
            case State.LOCKED:
                spriteRender.sprite = locked;
                buttonUI.enabled = false;
                boxCollider.enabled = false;
                circleCollider.enabled = false;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.UNLOCKED)
        {
            if(collision.gameObject.GetComponent<LevelSelectionKey>() != null)
            {
                Open();
                Destroy(collision.gameObject);
            }
        }
    }

    private void Open()
    {
        state = State.OPENED;
        spriteRender.sprite = buttonUp;
        SetLocker();
    }
}
