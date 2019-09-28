using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionCancelPrompt : FunctionUI
{

    FunctionPromptSpawn fps;

    public void SetPromptSpawner(FunctionPromptSpawn f)
    {
        fps = f;
    }

    public override void Function()
    {
        fps.ResetUIColliders();
        Destroy(transform.parent.gameObject);
    }
}
