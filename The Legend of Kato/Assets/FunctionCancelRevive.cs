using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionCancelRevive : FunctionUI
{
    public override void Function()
    {
        FindObjectOfType<Revive>().Cancel();
    }
}
