using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FunctionLoadLevel : FunctionUI
{
    [SerializeField] int SceneID;
    [SerializeField] bool showLoader = false;

    public override void Function()
    {
        if (!showLoader)
        {
            SceneManager.LoadScene(SceneID);
        }
    }
}
