using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionSaveSettings : FunctionUI
{
    public override void Function()
    {
        PlayerPrefs.Save();
        FindObjectOfType<AudioManager>().LoadSettings();
    }
}
