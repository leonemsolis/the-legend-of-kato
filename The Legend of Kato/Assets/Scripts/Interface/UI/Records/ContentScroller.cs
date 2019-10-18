using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScroller : MonoBehaviour
{
    Transform content;
    Vector3 mouseOrigin;
    BoxCollider2D boxCollider;

    SliderScroller scroller;
    Vector2 boxColliderDefaultSize;

    const float high = 724f;
    const float low = -563f;

    void Start()
    {
        content = transform.GetChild(0);
        boxCollider = GetComponent<BoxCollider2D>();
        boxColliderDefaultSize = new Vector2(boxCollider.size.x, boxCollider.size.y);
        mouseOrigin = Vector3.zero;
        scroller = FindObjectOfType<SliderScroller>();
    }

    private void OnMouseDown()
    {
        if (CheckTouch(Input.mousePosition))
        {
            mouseOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        boxCollider.size = boxColliderDefaultSize * 100f;
    }

    private void OnMouseDrag()
    {
        if (CheckTouch(Input.mousePosition))
        {
            Vector3 current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 delta = current - mouseOrigin;
            mouseOrigin = current;

            content.position = new Vector3(content.position.x, content.position.y + delta.y, 0f);

            if (content.localPosition.y > high)
            {
                content.localPosition = new Vector3(content.localPosition.x, high, 0f);
            }
            if (content.localPosition.y < low)
            {
                content.localPosition = new Vector3(content.localPosition.x, low, 0f);
            }
            scroller.SetPosition(-(content.localPosition.y - high) / (high - low));
        }
    }


    // 0 - top, 100 - bottom
    public void SetPosition(float percentageY)
    {
        float y = low + (high - low) * percentageY;
        content.localPosition = new Vector3(content.localPosition.x, y, 0f);

        if (content.localPosition.y > high)
        {
            content.localPosition = new Vector3(content.localPosition.x, high, 0f);
        }
        if (content.localPosition.y < low)
        {
            content.localPosition = new Vector3(content.localPosition.x, low, 0f);
        }
    }

    private void OnMouseUp()
    {
        boxCollider.size = boxColliderDefaultSize;
    }

    private bool CheckTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        return hit && hit == gameObject.GetComponent<Collider2D>();
    }
}
