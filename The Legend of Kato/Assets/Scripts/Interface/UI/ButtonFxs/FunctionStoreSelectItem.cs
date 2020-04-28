using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionStoreSelectItem : FunctionUI
{
    [SerializeField] int index;
    Store store;

    private void Start()
    {
        store = FindObjectOfType<Store>();
    }

    public override void Function()
    {
        store.SelectCertainItem(index);
    }
}
