using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShop : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckTouch(Input.mousePosition))
            {
                GetComponent<ButtonClickSound>().PlayClick();
                GetComponent<FunctionUI>().Function();
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

}
