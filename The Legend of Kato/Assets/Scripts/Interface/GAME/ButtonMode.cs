using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode { DEFAULT, SHOP, PAUSE }

public class ButtonMode : MonoBehaviour
{
    Mode mode;

    void Start()
    {
        mode = Mode.DEFAULT;
    }

    public void ChangeMode(Mode newMode)
    {
        mode = newMode;
    }

    public Mode GetMode()
    {
        return mode;
    }
}
