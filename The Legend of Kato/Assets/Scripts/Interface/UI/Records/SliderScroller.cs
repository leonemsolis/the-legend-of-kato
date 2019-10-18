using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScroller : MonoBehaviour
{
    Transform content;
    BoxCollider2D boxCollider;
    Vector2 boxColliderDefaultSize;
    ContentScroller scroller;

    const float high = 354f;
    const float low = -345f;

    void Start()
    {
        content = transform.GetChild(0);
        boxCollider = GetComponent<BoxCollider2D>();
        boxColliderDefaultSize = new Vector2(boxCollider.size.x, boxCollider.size.y);
        scroller = FindObjectOfType<ContentScroller>();
    }

    private void OnMouseDrag()
    {
        boxCollider.size = boxColliderDefaultSize * 100f;

        if (CheckTouch(Input.mousePosition))
        {
            Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            content.position = new Vector3(content.position.x, mPos.y, 0f);

            if (content.localPosition.y > high)
            {
                content.localPosition = new Vector3(content.localPosition.x, high, 0f);
            }

            if (content.localPosition.y < low)
            {
                content.localPosition = new Vector3(content.localPosition.x, low, 0f);
            }

            scroller.SetPosition(-(content.localPosition.y + low) / (high - low));
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
