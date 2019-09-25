using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderBGScaler : MonoBehaviour
{

    [SerializeField] bool top;
    const float centerPartHeight = 100f;


    private void Start()
    {
        float freeSpace = (Camera.main.orthographicSize * 2f - centerPartHeight) / 2f;
        float gap = freeSpace / 2f + centerPartHeight / 2f;

        if (top)
        {
            transform.localPosition = new Vector3(0f, gap, 0f);
            transform.localScale = new Vector3(1f, freeSpace / 100f, 0f);
        }
        else
        {
            transform.localPosition = new Vector3(0, -gap, 0f);
            transform.localScale = new Vector3(1f, freeSpace / 100f, 0f);
        }
    }
}
