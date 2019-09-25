using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionAudioToggle : FunctionUI
{
    [SerializeField] bool musicButton;

    bool on;

    private void Start()
    {
        if (musicButton)
        {
            on = PlayerPrefs.GetInt(C.PREFS_MUSIC, 1) == 1;
        }
        else
        {
            on = PlayerPrefs.GetInt(C.PREFS_SOUNDS, 1) == 1;
        }
    }

    public override void Function()
    {
        on = !on;

        if (musicButton)
        {
            if(on)
            {
                PlayerPrefs.SetInt(C.PREFS_MUSIC, 1);
            }
            else
            {
                PlayerPrefs.SetInt(C.PREFS_MUSIC, 0);
            }
        }
        else
        {
            if (on)
            {
                PlayerPrefs.SetInt(C.PREFS_SOUNDS, 1);
            }
            else
            {
                PlayerPrefs.SetInt(C.PREFS_SOUNDS, 0);
            }
        }
    }
}
