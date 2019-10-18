using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBackToMainAsync : FunctionUI
{
    public override void Function()
    {
        FindObjectOfType<Blackout>().LoadSceneAsynchronously(C.MainMenuSceneIndex);
    }
}
