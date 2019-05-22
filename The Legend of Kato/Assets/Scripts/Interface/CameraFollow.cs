using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    const float delayTime = .2f;
    public readonly static float unitsToBottom = 1100f;

    PlayerRoomDetector roomDetector;
    PlayerController player;
    Vector3 v = Vector3.zero;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        roomDetector = FindObjectOfType<PlayerRoomDetector>();
    }

    private void Update()
    {

        Vector3 destination = Vector3.zero;
        switch(roomDetector.GetCurrentRoom().GetRoomType())
        {
            case RoomType.V:
                float shiftedY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottom;
                destination = new Vector3(roomDetector.GetCurrentRoom().transform.position.x, shiftedY, transform.position.z);
                break;
            case RoomType.H:
            case RoomType.VH:
            case RoomType.HV:
                destination = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
                break;
        }
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref v, delayTime);
    }
}