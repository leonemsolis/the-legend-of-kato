using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActive : MonoBehaviour
{
    PlayerController player;
    readonly RuntimePlatform platform = Application.platform;
    Pause pause;
    SpriteRenderer spriteRenderer;
    bool pressed = false;

    Color defaultColor = new Color(0.0627451f, 0.3686275f, 0.6941177f, 0.8941177f);
    Color pressedColor = new Color(0.4627451f, 0.3686275f, 0.6941177f, 0.8941177f);

    private void Start()
    {
        pause = FindObjectOfType<Pause>();
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(pause.IsGameRunning())
        {
            UpdateRunning();
        }
        else
        {
            UpdatePause();
        }
    }

    private void UpdateRunning()
    {
        if (pressed) { player.Jump(); spriteRenderer.color = pressedColor; }
        else { spriteRenderer.color = defaultColor; }


        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            bool checkPressed = false;
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase != TouchPhase.Ended && Input.GetTouch(i).phase != TouchPhase.Canceled)
                {
                    checkPressed = checkPressed || CheckTouch(Input.GetTouch(i).position);
                }
            }
            pressed = checkPressed;
        }
        else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButton(0))
            {
                if (CheckTouch(Input.mousePosition))
                {
                    pressed = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (CheckTouch(Input.mousePosition))
                {
                    pressed = false;
                }
            }
            if(Input.GetKey(KeyCode.L))
            {
                pressed = true;
            }
            if(Input.GetKeyUp(KeyCode.L))
            {
                pressed = false;
            }
        }
    }

    private void UpdatePause()
    {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    if(CheckTouch(Input.GetTouch(i).position))
                    {
                        ClickInPause();
                    }
                }
            }
        }
        else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(CheckTouch(Input.mousePosition))
                {
                    ClickInPause();
                }
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                ClickInPause();
            }
        }
    }

    private bool CheckTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);
        if (hit == gameObject.GetComponent<Collider2D>())
        {
            return true;
        }
        return false;
    }



    private void ClickInPause()
    {
        FindObjectOfType<PausePanel>().Activate();
        spriteRenderer.color = pressedColor;
        StartCoroutine(ResetColor());
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSecondsRealtime(.1f);
        spriteRenderer.color = defaultColor;
    }
}
