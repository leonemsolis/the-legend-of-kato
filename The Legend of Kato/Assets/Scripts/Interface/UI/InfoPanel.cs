using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    void Update()
    {
        float y = Camera.main.transform.position.y + Camera.main.orthographicSize - 1f;
        transform.position = new Vector3(5f, y, transform.position.z);
    }
}
