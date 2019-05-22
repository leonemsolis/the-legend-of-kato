using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] public List<Room> v_rooms;
    [SerializeField] public List<Room> h_rooms;
    [SerializeField] public List<Room> vh_rooms;
    [SerializeField] public List<Room> hv_rooms;

    PlayerRoomDetector player;

    const float unitSize = 100f;
    const float leftWallX = -5.5f * unitSize;
    const float rightWallX = 5.5f * unitSize;



    private void Start()
    {
        player = FindObjectOfType<PlayerRoomDetector>();
    }

    private void Update()
    {
        player.GetCurrentRoom().GenerateNext(this);
    }

}
