using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal.Input;

public class Button : MonoBehaviour
{
    [SerializeField] Sprite up;
    [SerializeField] Sprite down;
    [SerializeField] bool activeButton;

    readonly RuntimePlatform platform = Application.platform;

    SpriteRenderer spriteRenderer;
    ButtonClickSound clickSound;
    ButtonMode buttonMode;

    PlayerController player;

    private void Start()
    {
        buttonMode = GetComponent<ButtonMode>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        clickSound = GetComponent<ButtonClickSound>();

        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            for(int i = 0; i < Input.touchCount; ++i)
            {
                if(Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    CheckTouch(Input.GetTouch(i).position);
                }
            }
        }
        else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckTouch(Input.mousePosition);
            }
            if(activeButton)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    TouchButton();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    TouchButton();
                }
            }
        }
    }

    private void CheckTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        if (hit && hit == gameObject.GetComponent<Collider2D>())
        {
            TouchButton();
        }
    }


    private void TouchButton()
    {
        PressButton();
        if(activeButton)
        {
            DoActive();
        }
        else
        {
            DoMove();
        }
    }

    private void DoActive()
    {
        switch(buttonMode.GetMode())
        {
            case Mode.DEFAULT:
                player.Jump();
                break;
            case Mode.PAUSE:
                // Use this type of access, because pause panel is not always enabled
                FindObjectOfType<PausePanel>().Activate();
                break;
            case Mode.SHOP:
                // Use this type of access, because shop is not always exits
                FindObjectOfType<Shop>().BuyItem();
                FindObjectOfType<FunctionBuy>().ActivateRemotely();
                break;
           
        }
    }

    private void DoMove()
    {
        switch(buttonMode.GetMode())
        {
            case Mode.DEFAULT:
                player.ChangeDirection();
                break;
            case Mode.PAUSE:
                // Use this type of access, because pause panel is not always enabled
                FindObjectOfType<PausePanel>().SelectNextElement();
                break;
            case Mode.SHOP:
                // Use this type of access, because shop is not always exits
                FindObjectOfType<Shop>().SelectNextItem();
                break;
        }
    }

    public void PressButton()
    {
        clickSound.PlayClick();
        spriteRenderer.sprite = down;
        StartCoroutine(ResetButton());
    }

    private IEnumerator ResetButton()
    {
        yield return new WaitForSecondsRealtime(.1f);
        spriteRenderer.sprite = up;
    }
}
