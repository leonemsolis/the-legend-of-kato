using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{

    [SerializeField] Sprite boots;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null;
    }

    public void SetItem(Upgrade u)
    {
        switch(u)
        {
            case Upgrade.NONE:
                spriteRenderer.sprite = null;
                break;
            case Upgrade.BOOTS:
                spriteRenderer.sprite = boots;
                break;
        }
    }
}
