using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionPromptResetSpawn : FunctionUI
{
    [SerializeField] GameObject prompt;

    public override void Function()
    {
        foreach(BoxCollider2D b in FindObjectsOfType<BoxCollider2D>())
        {
            b.GetComponent<BoxCollider2D>().enabled = false;
        }
        Instantiate(prompt, Vector3.zero, Quaternion.identity);
    }
}
