using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanguageToggleUI : MonoBehaviour
{
    [SerializeField] Sprite enUp;
    [SerializeField] Sprite enDown;
    [SerializeField] Sprite krUp;
    [SerializeField] Sprite krDown;
    
    SpriteRenderer spriteRenderer;
    Translator.Language language;
    bool pressed = false;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        language = FindObjectOfType<Translator>().GetLanguage();
        ChangeSprite();
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
                ButtonReleased(true);
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
        if(!pressed) {
            if(GetComponent<ButtonClickSound>() != null)
            {
                GetComponent<ButtonClickSound>().PlayClick();
            }
            pressed = true;
            ChangeSprite();
        }
    }

    void ButtonReleased(bool activated)
    {
        pressed = false;
        if(activated) {
            switch(language) {
                case Translator.Language.English:
                    FindObjectOfType<Translator>().SetLanguage(Translator.Language.Korean); 
                    break;
                case Translator.Language.Korean:
                    FindObjectOfType<Translator>().SetLanguage(Translator.Language.English);
                    break;
            }
            language = FindObjectOfType<Translator>().GetLanguage();
            ChangeSprite();
            FindObjectOfType<Blackout>().LoadScene(C.SettingsSceneIndex);
            GetComponent<BoxCollider2D>().enabled = false;
            return;
        }
        ChangeSprite();
    }

    private void ChangeSprite() {
        Sprite s = null;
        switch(language) {
            case Translator.Language.English:
                s = pressed ? krDown : krUp;
                break;
            case Translator.Language.Korean:
                s = pressed ? enDown : enUp;
                break;
        }
        if(s) spriteRenderer.sprite = s; 
    }
}
