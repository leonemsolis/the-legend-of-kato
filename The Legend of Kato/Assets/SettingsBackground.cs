using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBackground : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float scale = screenHeight / (10f * 100f);
        transform.localScale = new Vector3(1f, scale, 1f);
    }
}
