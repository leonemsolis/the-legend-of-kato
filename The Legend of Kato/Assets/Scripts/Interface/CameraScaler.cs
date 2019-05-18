﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    void Awake()
    {
        float unitPhysicPixelSize = Screen.width / 1000f;
        float cameraHeight = Screen.height / unitPhysicPixelSize;
        float cameraSize = cameraHeight / 2f;
        Camera.main.orthographicSize = cameraSize;
    }
}
