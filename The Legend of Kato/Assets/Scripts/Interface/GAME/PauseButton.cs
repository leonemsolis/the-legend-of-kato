#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    readonly RuntimePlatform platform = Application.platform;
    [SerializeField] Sprite pauseDown;
    [SerializeField] Sprite pauseUp;

    PausePanel pausePanel;

    ButtonMode mode;
    Mode lastMode = Mode.DEFAULT;

    SpriteRenderer spriteRenderer;
    ButtonClickSound clickSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        clickSound = GetComponent<ButtonClickSound>();
        mode = GetComponent<ButtonMode>();

        pausePanel = FindObjectOfType<PausePanel>();
        pausePanel.gameObject.SetActive(false);
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
        else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.WebGLPlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckTouch(Input.mousePosition);
            }
            if(gameObject.GetComponent<Collider2D>().enabled)
            {
                if (Input.GetKeyDown(KeyCode.Space))
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
        clickSound.PlayClick();
        if (mode.GetMode() == Mode.PAUSE)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        // Save current mode
        lastMode = FindObjectOfType<ButtonMode>().GetMode();
        // Change all buttons mode
        foreach (ButtonMode b in FindObjectsOfType<ButtonMode>())
        {
            b.ChangeMode(Mode.PAUSE);
        }
        spriteRenderer.sprite = pauseDown;

        Time.timeScale = 0f;
        pausePanel.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        spriteRenderer.sprite = pauseUp;
        // Restore last button mode
        foreach (ButtonMode b in FindObjectsOfType<ButtonMode>())
        {
            b.ChangeMode(lastMode);
        }

        Time.timeScale = C.DefaulTimeScale;
        pausePanel.gameObject.SetActive(false);
    }
}
