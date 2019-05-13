using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    private void Update()
    {
        float y = Camera.main.transform.position.y - Camera.main.orthographicSize + 4f / 2f;
        transform.position = new Vector3(5f, y, transform.position.z);
    }
}
