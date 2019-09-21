using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSelector : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool on = false;
    const float period = .5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        yield return new WaitForSeconds(period);
        spriteRenderer.enabled = on;
        on = !on;
        StartCoroutine(Blink());
    }
}
