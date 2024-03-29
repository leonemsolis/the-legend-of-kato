﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionKey : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    Vector2 origin;
    bool dragging = false;

    public int keyPrefIndex = 0;
    int tier = 1;

    private void Start()
    {
        origin = transform.position;
    }

    public void SetTier(int t)
    {
        tier = t;
        GetComponent<SpriteRenderer>().sprite = sprites[t - 1];
    }

    public int GetTier()
    {
        return tier;
    }

    void Update()
    {
        if(dragging)
        {
            transform.position = GetMousePos();
        }
        else
        {
            origin = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is CircleCollider2D)
        {
            if(collision.GetComponent<LevelsLock>().Open(this))
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnMouseUp()
    {
        if(dragging)
        {
            dragging = false;
            transform.position = origin;
        }
    }

    private void OnMouseDown()
    {
        if (CheckTouch())
        {
            dragging = true;
        }
    }

    private bool CheckTouch()
    {
        Collider2D hit = Physics2D.OverlapPoint(GetMousePos());
        return hit && hit == gameObject.GetComponent<Collider2D>();
    }

    private Vector2 GetMousePos()
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        return touchPos;
    }
}
