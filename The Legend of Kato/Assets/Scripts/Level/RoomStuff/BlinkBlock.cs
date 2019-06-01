using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BlinkState { VISIBLE, HALF, INVISIBLE}

public class BlinkBlock : MonoBehaviour
{
    [SerializeField] float firstBlinkDelay = 0f;
    [SerializeField] float blinkTime = 1f;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    BlinkState blinkState = BlinkState.VISIBLE;
    Color white = new Color(1f, 1f, 1f);
    Color gray = new Color(0.7921569f, 0.7921569f, 0.7921569f);
    Color black = new Color(0.3529412f, 0.3529412f, 0.3529412f);


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(WaitFirst());
    }

    private IEnumerator WaitFirst()
    {
        yield return new WaitForSeconds(firstBlinkDelay);
        StartCoroutine(Blink());
    }


    private IEnumerator Blink()
    {
        switch(blinkState)
        {
            case BlinkState.VISIBLE:
                yield return new WaitForSeconds(blinkTime);
                blinkState = BlinkState.HALF;
                spriteRenderer.color = gray;
                StartCoroutine(Blink());
                break;
            case BlinkState.HALF:
                yield return new WaitForSeconds(blinkTime);
                blinkState = BlinkState.INVISIBLE;
                boxCollider.enabled = false;
                spriteRenderer.color = black;
                StartCoroutine(Blink());
                break;
            case BlinkState.INVISIBLE:
                yield return new WaitForSeconds(blinkTime);
                blinkState = BlinkState.VISIBLE;
                boxCollider.enabled = true;
                spriteRenderer.color = white;
                StartCoroutine(Blink());
                break;
        }
    }

}
