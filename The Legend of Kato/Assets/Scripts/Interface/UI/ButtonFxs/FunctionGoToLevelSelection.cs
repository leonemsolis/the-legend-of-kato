using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionGoToLevelSelection : FunctionUI
{
    [SerializeField] bool practiceMode;

    public override void Function()
    {
        if(practiceMode)
        {
            PlayerPrefs.SetInt(C.PREFS_PRACTICE_MODE, 1);
        }
        else
        {
            PlayerPrefs.SetInt(C.PREFS_PRACTICE_MODE, 0);
        }

        FindObjectOfType<Blackout>().LoadScene(C.LevelSelectionSceneIndex);
    }
}
