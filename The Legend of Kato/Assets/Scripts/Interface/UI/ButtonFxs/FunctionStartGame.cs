using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionStartGame : FunctionUI
{
    public override void Function()
    {
        FindObjectOfType<ItemSelector>().StartGame();
    }
}
