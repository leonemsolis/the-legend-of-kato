using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionConfirmRevive : FunctionUI
{
    public override void Function()
    {
        FindObjectOfType<Revive>().Confirm();
    }
}
