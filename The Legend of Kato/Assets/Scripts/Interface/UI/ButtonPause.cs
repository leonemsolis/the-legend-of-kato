using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPause : MonoBehaviour
{
    readonly RuntimePlatform platform = Application.platform;
    [SerializeField] Sprite pause;
    [SerializeField] Sprite cont;
    SpriteRenderer spriteRenderer;
    bool running = true;

    private void Start()
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
        if(running)
        {
            Time.timeScale = 0f;
            spriteRenderer.sprite = cont;
        }
        else
        {
            Time.timeScale = 1f;
            spriteRenderer.sprite = pause;
        }
        running = !running;
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.8941177f);
    }
}
