using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsLock : MonoBehaviour
{
    enum State {OPENED, UNLOCKED, LOCKED};

    [SerializeField] public int LockID;

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
            case 0:
            case 1:
                state = State.OPENED;
                break;
            case 2:
                int stage_2_state = PlayerPrefs.GetInt(C.PREFS_STAGE_2_OPENED, 0);
                if (stage_2_state == 0)
                {
                    state = State.LOCKED;
                }
                else
                {
                    state = State.UNLOCKED;
                }
                break;
            case 3:
                //TODO:REMOVE
                //int stage_3_state = PlayerPrefs.GetInt(C.PREFS_STAGE_3_OPENED, 0);
                int stage_3_state = 1;
                if (stage_3_state == 0)
                {
                    state = State.LOCKED;
                }
                else
                {
                    state = State.UNLOCKED;
                }
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
                if(LockID == 2)
                {
                    GameObject.FindWithTag("LevelSelectionMask2").SetActive(false);
                }
                if(LockID == 3)
                {
                    GameObject.FindWithTag("LevelSelectionMask3").SetActive(false);
                }
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
                GetComponent<FunctionLoadLevel>().keyTier = collision.gameObject.GetComponent<LevelSelectionKey>().GetTier();
                GetComponent<FunctionLoadLevel>().keyIndex = collision.gameObject.GetComponent<LevelSelectionKey>().keyPrefIndex;
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
