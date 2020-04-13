using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().pointLightOuterRadius = Camera.main.orthographicSize + 50f;   
        transform.localScale = new Vector3(14f, Camera.main.orthographicSize / 100f * 2f + 4f, 1f);
    }
}
