using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Transform selector;
    const float selectorDeltaX = 150f;
    int selectorCurrentPosition = 0;

    void Start()
    {
        selector = transform.GetChild(0);
    }

    public void SelectNextItem()
    {
        selectorCurrentPosition++;
        if(selectorCurrentPosition == 4)
        {
            selector.position += new Vector3(-selectorDeltaX * 3, 0f, 0f);
            selectorCurrentPosition = 0;
        }
        else
        {
            selector.position += new Vector3(selectorDeltaX, 0f, 0f);
        }
    }

    public void BuyItem()
    {
        Debug.Log("BOUGHT ITEM");
    }
}
