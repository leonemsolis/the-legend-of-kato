using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    Pause pause;
    PlayerController player;
    readonly RuntimePlatform platform = Application.platform;
    SpriteRenderer spriteRenderer;

    Color defaultColor = new Color(0.5686275f, 0.09411766f, 0.1215686f, 0.8941177f);
    Color pressedColor = new Color(0.3686275f, 0.09411766f, 0.1215686f, 0.8941177f);

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        pause = FindObjectOfType<Pause>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (Input.GetKeyDown(KeyCode.A))
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
        Blink();
        if (pause.IsGameRunning())
        {
            player.ChangeDirection();
        }
        else
        {
            FindObjectOfType<PausePanel>().SelectNextElement();
        }
    }

    public void Blink()
    {
        spriteRenderer.color = pressedColor;
        StartCoroutine(ResetColor());
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSecondsRealtime(.1f);
        spriteRenderer.color = defaultColor;
    }
}
