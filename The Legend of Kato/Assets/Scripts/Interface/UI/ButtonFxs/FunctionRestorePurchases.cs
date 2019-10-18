using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionRestorePurchases : FunctionUI
{
    public override void Function()
    {
        FindObjectOfType<Purchaser>().RestorePurchases();
    }
}
