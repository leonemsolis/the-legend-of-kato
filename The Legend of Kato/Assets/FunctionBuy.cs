using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBuy : FunctionUI
{
    [SerializeField] Sprite up;
    [SerializeField] Sprite down;

    SpriteRenderer spriteRenderer;
    Shop shop;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shop = FindObjectOfType<Shop>();
    }

    public override void Function()
    {
        shop.BuyItem();
    }

    public void ActivateRemotely()
    {
        spriteRenderer.sprite = down;
        StartCoroutine(ResetButton());
    }

    private IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(.1f);
        spriteRenderer.sprite = up;
    }
}
