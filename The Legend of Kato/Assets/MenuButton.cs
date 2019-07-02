using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] Sprite up;
    [SerializeField] Sprite down;
    [SerializeField] bool activeButton;

    readonly RuntimePlatform platform = Application.platform;

    SpriteRenderer spriteRenderer;
    ButtonClickSound clickSound;
    MainMenu mainMenu;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        clickSound = GetComponent<ButtonClickSound>();
        mainMenu = FindObjectOfType<MainMenu>();
    }

    void Update()
    {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
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
            if (activeButton)
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
        if (activeButton)
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
        mainMenu.ActivateCurrentElement();
    }

    private void DoMove()
    {
        mainMenu.SelectNextElement();
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
