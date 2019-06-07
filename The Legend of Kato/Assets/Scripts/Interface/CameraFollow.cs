using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    const float delayTime = .3f;
    public readonly static float unitsToBottom = 1000f;
    public readonly static float unitsToBottomInRoom = 800f;

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
        float posX = 0;
        float posY = 0;

        if (roomDetector.GetCurrentRoom() != null)
        {
            posX = roomDetector.GetCurrentRoom().transform.position.x;
            if (player.transform.position.y - roomDetector.GetCurrentRoom().transform.position.y > -350)
            {
                posY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottomInRoom;
            }
            else
            {
                posY = roomDetector.GetCurrentRoom().transform.position.y + Camera.main.orthographicSize - 750f - 400f;
            }
        } 
        else
        {
            posX = player.IsFacingRight() ? player.transform.position.x + 300f : player.transform.position.x - 300f;
            posY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottom;
        }


        Vector3 destination = new Vector3(posX, posY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref v, delayTime);
    }
}