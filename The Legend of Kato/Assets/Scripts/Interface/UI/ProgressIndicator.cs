using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressIndicator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Sprite currentSprite;
    bool changed = true;
    bool active = false;

    const float period = .5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }


    private IEnumerator Blink()
    {
        yield return new WaitForSeconds(period);
        spriteRenderer.enabled = active;
        if(active)
        {
            if(!changed)
            {
                spriteRenderer.sprite = currentSprite;
                changed = true;
            }
        }
        active = !active;
        StartCoroutine(Blink());
    }

    public void ChangeSprite(Sprite s)
    {
        currentSprite = s;
        changed = false;
    }
}
