using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceFollow : MonoBehaviour
{
    Transform target;
    float deltaY;

    void Start()
    {
        target = Camera.main.transform;
        deltaY = transform.position.y - target.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + deltaY, transform.position.z);
    }
}
