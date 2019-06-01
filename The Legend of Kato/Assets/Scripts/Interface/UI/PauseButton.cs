#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    readonly RuntimePlatform platform = Application.platform;
    [SerializeField] Sprite pauseDown;
    [SerializeField] Sprite pauseUp;

    SpriteRenderer spriteRenderer;
    ButtonClickSound clickSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        clickSound = GetComponent<ButtonClickSound>();
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
            if(Input.GetKeyDown(KeyCode.Space))
            {
                TouchButton();
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
        clickSound.PlayClick();
        if (GetComponent<Pause>().IsGameRunning())
        {
            spriteRenderer.sprite = pauseDown;
            GetComponent<Pause>().PauseGame();
        }
        else
        {
            spriteRenderer.sprite = pauseUp;
            GetComponent<Pause>().ResumeGame();
        }
    }
}
