using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    const float delayTime = .2f;
    public readonly static float unitsToBottom = 11f;

    PlayerController player;
    Vector3 v = Vector3.zero;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        float shiftedY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottom;
        Vector3 destination = new Vector3(transform.position.x, shiftedY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref v, delayTime);
    }
}