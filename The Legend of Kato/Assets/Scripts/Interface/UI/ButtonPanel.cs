using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    private void Start()
    {
        float y = Camera.main.transform.position.y - Camera.main.orthographicSize + 4f / 2f;
        transform.position = new Vector3(0f, y, transform.position.z);
    }
}
