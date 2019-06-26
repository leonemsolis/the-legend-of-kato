using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] int roomID;
    [SerializeField] GameObject exitBlock;
    [SerializeField] List<GameObject> door;
    [SerializeField] float roomHeight = 1500f;
    const float RoomEraseLeftDistance = 600f;

    public float RoomHeight
    {
        get
        {
            return roomHeight;
        }
    }

    [SerializeField] float roomWidth = 1200f;

    public float RoomWidth
    {
        get
        {
            return roomWidth;
        }
    }

    List<string> erasable;

    bool closed = false;

    public bool Closed
    {
        get
        {
            return closed;
        }
    }

    public int RoomID
    {
        get
        {
            return roomID;
        }
    }

    private void Awake()
    {
        foreach(GameObject g in door)
        {
            g.SetActive(false);
        }

        erasable = new List<string>
        {
            C.BlockTag,
            C.RoomTag,
            C.InvulnirableEnemyHitBoxTag
        };
    }

    public GameObject GetExitBlock()
    {
        return exitBlock;
    }

    public void CloseDoor()
    {
        // Close door
        foreach (GameObject g in door)
        {
            g.SetActive(true);
            closed = true;
        }
    }
}
