using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActive : MonoBehaviour
{
    PlayerController player;
    readonly RuntimePlatform platform = Application.platform;
    Pause pause;
    SpriteRenderer spriteRenderer;

    Color defaultColor = new Color(0.0627451f, 0.3686275f, 0.6941177f, 0.8941177f);
    Color pressedColor = new Color(0.0627451f, 0.3686275f, 0.4941177f, 0.8941177f);

    private void Start()
    {
        pause = FindObjectOfType<Pause>();
        player = FindObjectOfType<PlayerController>();
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
            if (Input.GetKeyDown(KeyCode.L))
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
            player.Jump();
        }
        else
        {
            FindObjectOfType<PausePanel>().Activate();
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
