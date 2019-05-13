using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalFollower : MonoBehaviour
{

    [SerializeField] bool isCamera = true;
    Transform target;
    float deltaY;

    void Start()
    {
        if (isCamera)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;
            deltaY = 8f - Camera.main.orthographicSize;
        }
        else
        {
            target = Camera.main.transform;
            deltaY = Camera.main.orthographicSize;
        }
    }

    private void Update()
    {
        if (isCamera)
        {
            deltaY = 8f - Camera.main.orthographicSize;
        }
        transform.position = new Vector3(transform.position.x, target.position.y - deltaY, transform.position.z);
    }
}
