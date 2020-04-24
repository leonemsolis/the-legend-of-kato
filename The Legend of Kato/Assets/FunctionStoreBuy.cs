using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionStoreBuy : FunctionUI
{
    private Store store;

    private void Awake() {
        store = FindObjectOfType<Store>();
    }

    public override void Function()
    {
        store.BuyItem();
    }
}
