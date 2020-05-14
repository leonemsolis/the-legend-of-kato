using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGameQuit : MonoBehaviour
{
    [SerializeField] bool disableRepeat = false;

    [SerializeField] Sprite up;
    [SerializeField] Sprite down;
    [SerializeField] int upID;
    [SerializeField] int downID;
    
    bool pressed = false;
    

    SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        up = FindObjectOfType<SpriteTranslator>().GetSprite(up, upID);
        down = FindObjectOfType<SpriteTranslator>().GetSprite(down, downID);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = up;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckTouch(Input.mousePosition))
            {
                ButtonPressed();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (pressed)
            {
                if (CheckTouch(Input.mousePosition))
                {
                    Activate();
                }
                else
                {
                    ButtonReleased();
                }
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
        if (GetComponent<ButtonClickSound>() != null)
        {
            GetComponent<ButtonClickSound>().PlayClick();
        }
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().enabled = false;
        }
        spriteRenderer.sprite = down;
        pressed = true;
    }

    void ButtonReleased()
    {
        spriteRenderer.sprite = up;
        pressed = false;
        if (GetComponent<Animator>() != null)
        {
            GetComponent<Animator>().enabled = true;
        }
    }

    void Activate()
    {
        if(disableRepeat) {
            GetComponent<Collider2D>().enabled = false;
        }
        ButtonReleased();
        GetComponent<FunctionUI>().Function();
    }
}
