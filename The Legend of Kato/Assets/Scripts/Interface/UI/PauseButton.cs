#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    readonly RuntimePlatform platform = Application.platform;
    [SerializeField] Sprite pauseDown;
    [SerializeField] Sprite pauseUp;
    [SerializeField] Sprite contDown;
    [SerializeField] Sprite contUp;
    SpriteRenderer spriteRenderer;

    void Start()
    {
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
        if (GetComponent<Pause>().IsGameRunning())
        {
            spriteRenderer.sprite = pauseDown;
            StartCoroutine(ResetButton(true));
            GetComponent<Pause>().PauseGame();
        }
        else
        {
            spriteRenderer.sprite = contDown;
            StartCoroutine(ResetButton(false));
            GetComponent<Pause>().ResumeGame();
        }
    }

    private IEnumerator ResetButton(bool pausePressed)
    {
        yield return new WaitForSecondsRealtime(.1f);
        spriteRenderer.sprite = pausePressed ? contUp : pauseUp;
    }
}
