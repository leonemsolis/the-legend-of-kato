using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionResumeGame : FunctionUI
{
    public override void Function()
    {
        FindObjectOfType<PauseButton>().ResumeGame();
    }
}
