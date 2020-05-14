using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsToggleUI : MonoBehaviour
{
    [SerializeField] Sprite onUp;
    [SerializeField] Sprite onDown;
    [SerializeField] Sprite offUp;
    [SerializeField] Sprite offDown;

    [SerializeField] bool isMusic;

    bool pressed = false;

    bool on = true;

    SpriteRenderer spriteRenderer;
    Translator.Language language;

    private void OnEnable()
    {
        onUp = FindObjectOfType<SpriteTranslator>().GetSprite(onUp, 0);
        onDown = FindObjectOfType<SpriteTranslator>().GetSprite(onDown, 1);
        offUp = FindObjectOfType<SpriteTranslator>().GetSprite(offUp, 2);
        offDown = FindObjectOfType<SpriteTranslator>().GetSprite(offDown, 3);

        spriteRenderer = GetComponent<SpriteRenderer>();

        if(isMusic)
        {
            on = PlayerPrefs.GetInt(C.PREFS_MUSIC, 1) == 1;
        }
        else
        {
            on = PlayerPrefs.GetInt(C.PREFS_SOUNDS, 1) == 1;
        }
        if (on)
        {
            spriteRenderer.sprite = onUp;
        } else {
            spriteRenderer.sprite = offUp;
        }
    }

    private void OnMouseDown()
    {
        if (CheckTouch(Input.mousePosition))
        {
            ButtonPressed();
        }
    }

    private void OnMouseUp()
    {
        if (pressed)
        {
            if (CheckTouch(Input.mousePosition))
            {
                Activate();
            }
            else
            {
                ButtonReleased(false);
            }
        }
    }


    private bool CheckTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        return hit && hit == gameObject.GetComponent<Collider2D>();
    }


    void ButtonPressed()
    {
        if(GetComponent<ButtonClickSound>() != null)
        {
            GetComponent<ButtonClickSound>().PlayClick();
        }
        spriteRenderer.sprite = on ? onDown : offDown;
        pressed = true;
    }

    void ButtonReleased(bool activated)
    {
        if(activated)
        {
            on = !on;
        }
        spriteRenderer.sprite = on ? onUp : offUp;
        pressed = false;
    }

    void Activate()
    {
        ButtonReleased(true);
        GetComponent<FunctionUI>().Function();
    }
}
