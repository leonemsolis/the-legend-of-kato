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
        float camY = 0f;
        switch (roomDetector.GetCurrentRoom().GetRoomType())
        {
            case RoomType.V:
            case RoomType.HV:
                camY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottom;
                break;
            case RoomType.H:
            case RoomType.VH:
                if(player.transform.position.y - roomDetector.GetCurrentRoom().transform.position.y > 0)
                {
                    camY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottom;
                }
                else
                {
                    camY = roomDetector.GetCurrentRoom().transform.position.y + Camera.main.orthographicSize - 750f - 400f;
                }
                break;
        }

        Vector3 destination = new Vector3(roomDetector.GetCurrentRoom().transform.position.x, camY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref v, delayTime);
    }
}