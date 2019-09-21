﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] Sprite up;
    [SerializeField] Sprite down;

    bool pressed = false;

    readonly RuntimePlatform platform = Application.platform;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(CheckTouch(Input.mousePosition))
            {
                ButtonPressed();
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(pressed)
            {
                if(CheckTouch(Input.mousePosition))
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
        spriteRenderer.sprite = down;
        pressed = true;
    }

    void ButtonReleased()
    {
        spriteRenderer.sprite = up;
        pressed = false;
    }

    void Activate()
    {
        ButtonReleased();
        GetComponent<FunctionUI>().Function();
    }
}