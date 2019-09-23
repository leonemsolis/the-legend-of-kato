using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionSelectItem : FunctionUI
{
    [SerializeField] int index;
    Shop shop;

    private void Start()
    {
        shop = FindObjectOfType<Shop>();
    }

    public override void Function()
    {
        shop.SelectCertainItem(index);
    }
}
