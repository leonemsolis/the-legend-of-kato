using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActive : MonoBehaviour
{
    PlayerController player;
    readonly RuntimePlatform platform = Application.platform;
    SpriteRenderer spriteRenderer;
    bool pressed = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (pressed){ player.Jump(); spriteRenderer.color = Color.gray;}
        else{spriteRenderer.color = new Color(1f, 1f, 1f, 0.8941177f);}


        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            bool checkPressed = false;
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if(Input.GetTouch(i).phase != TouchPhase.Ended && Input.GetTouch(i).phase != TouchPhase.Canceled)
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
                if(CheckTouch(Input.mousePosition))
                {
                    pressed = true;
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                if(CheckTouch(Input.mousePosition))
                {
                    pressed = false;
                }
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

    public void SetButtonActiveColor(bool val)
    {
        pressed = val;
    }
}
