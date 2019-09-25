using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBgTop : MonoBehaviour
{
    const float topPartHeight = 387.5f;

    void Start()
    {
        transform.localPosition = new Vector3(0f, Camera.main.orthographicSize - topPartHeight / 2f, 0f);
    }
}
