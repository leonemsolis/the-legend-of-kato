using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] public List<Room> v_rooms;
    [SerializeField] public List<Room> h_rooms;
    [SerializeField] public List<Room> vh_rooms;
    [SerializeField] public List<Room> hv_rooms;

    List<RoomType> requests = new List<RoomType>();
    RoomType currentRequest = RoomType.NONE;

    PlayerRoomDetector player;

    const float unitSize = 100f;
    const float leftWallX = -5.5f * unitSize;
    const float rightWallX = 5.5f * unitSize;



    private void Start()
    {
        player = FindObjectOfType<PlayerRoomDetector>();
        ResetRequests();
        GenerateNextRequest();
    }

    private void Update()
    {
        if(player.GetCurrentRoom().GenerateNext(this, currentRequest))
        {
            GenerateNextRequest();
        }
    }

    private void GenerateNextRequest()
    {
        if (currentRequest != RoomType.NONE)
        {
            requests.Remove(currentRequest);
        }
        currentRequest = requests[Random.Range(0, requests.Count)];
        if (requests.Count == 1)
        {
            ResetRequests();
        }
    }

    private void ResetRequests()
    {
        //requests.Add(RoomType.H);
        //requests.Add(RoomType.H);
        //requests.Add(RoomType.H);
        //requests.Add(RoomType.H);
        //requests.Add(RoomType.V);
        //requests.Add(RoomType.V);
        //requests.Add(RoomType.V);
        //requests.Add(RoomType.V);
        //requests.Add(RoomType.V);
        //requests.Add(RoomType.V);
        //requests.Add(RoomType.V);
        //requests.Add(RoomType.V);

        requests.Add(RoomType.V);
        requests.Add(RoomType.V);
        requests.Add(RoomType.V);
        requests.Add(RoomType.V);
        requests.Add(RoomType.V);
        requests.Add(RoomType.V);
        requests.Add(RoomType.V);
        requests.Add(RoomType.V);
    }

}
