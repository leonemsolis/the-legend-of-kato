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
    ButtonUI buttonUI;

    void Start()
    {
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
                    if(PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 1)
                    {
                        state = State.OPENED;
                    }
                    else
                    {
                        state = State.UNLOCKED;
                    }
                }
                break;
            case 3:
                int stage_3_state = PlayerPrefs.GetInt(C.PREFS_STAGE_3_OPENED, 0);
                if (stage_3_state == 0)
                {
                    state = State.LOCKED;
                }
                else
                {
                    if (PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 1)
                    {
                        state = State.OPENED;
                    }
                    else
                    {
                        state = State.UNLOCKED;
                    }
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
                if (LockID == 2)
                {
                    if(GameObject.FindWithTag("LevelSelectionMask2") != null)
                    {
                        GameObject.FindWithTag("LevelSelectionMask2").SetActive(false);
                    }
                }
                if (LockID == 3)
                {
                    if (GameObject.FindWithTag("LevelSelectionMask3") != null)
                    {
                        GameObject.FindWithTag("LevelSelectionMask3").SetActive(false);
                    }
                }
                spriteRender.sprite = buttonUp;
                buttonUI.enabled = true;
                GetComponent<CircleCollider2D>().enabled = false;
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
                break;
            case State.LOCKED:
                spriteRender.sprite = locked;
                buttonUI.enabled = false;
                break;
        }
    }


    public bool Open(LevelSelectionKey key)
    {
        if(state == State.UNLOCKED)
        {
            GetComponent<FunctionLoadLevel>().keyTier = key.GetTier();
            GetComponent<FunctionLoadLevel>().keyIndex = key.keyPrefIndex;
            state = State.OPENED;
            SetLocker();
            return true;
        }
        return false;
    }
}
