using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType { V, H, VH, HV };
public enum Entrance { TOP, BOTTOM, LEFT, RIGHT };

public class Room : MonoBehaviour
{
    [SerializeField] RoomType roomType;
    [SerializeField] int roomID;
    [SerializeField] bool blockedTop;
    [SerializeField] bool blockedBottom;
    [SerializeField] bool blockedLeft;
    [SerializeField] bool blockedRight;
    [SerializeField] List<Vector2> topGates;
    [SerializeField] List<Vector2> bottomGates;
    [SerializeField] List<Vector2> leftGates;
    [SerializeField] List<Vector2> rightGates;
    bool extended = false;
    const float roomHeight = 1500f;
    const float roomWidth = 1200f;

    public void SetEntrance(Entrance entrance)
    {
        switch (entrance)
        {
            case Entrance.TOP:
                blockedTop = true;
                break;
            case Entrance.BOTTOM:
                blockedBottom = true;
                break;
            case Entrance.LEFT:
                blockedLeft = true;
                break;
            case Entrance.RIGHT:
                blockedRight = true;
                break;
        }
    }

    public void GenerateNext(LevelGenerator lg)
    {
        if (!extended)
        {
            GenerateLeftRoom(lg);
            GenerateRightRoom(lg);
            GenerateBottomRoom(lg);
            extended = true;
        }
    }

    private void GenerateLeftRoom(LevelGenerator lg)
    {
        if (!blockedLeft)
        {
            if(roomType == RoomType.H || roomType == RoomType.VH)
            {
                Vector3 newPosition = new Vector3(transform.position.x - roomWidth, transform.position.y, transform.position.z);
                // Generate H or HV
                List<Room> candidates = new List<Room>();
                candidates.AddRange(GetExtendCandidates(RoomType.H, Entrance.RIGHT, lg));
                candidates.AddRange(GetExtendCandidates(RoomType.HV, Entrance.RIGHT, lg));
                Room r = Instantiate(candidates[Random.Range(0, candidates.Count)], newPosition, Quaternion.identity);
                r.SetEntrance(Entrance.RIGHT);
            }
        }
    }

    private void GenerateRightRoom(LevelGenerator lg)
    {
        if (!blockedRight)
        {
            if (roomType == RoomType.H || roomType == RoomType.VH)
            {
                Vector3 newPosition = new Vector3(transform.position.x + roomWidth, transform.position.y, transform.position.z);
                // Generate H or HV
                List<Room> candidates = new List<Room>();
                candidates.AddRange(GetExtendCandidates(RoomType.H, Entrance.LEFT, lg));
                candidates.AddRange(GetExtendCandidates(RoomType.HV, Entrance.LEFT, lg));
                Room r = Instantiate(candidates[Random.Range(0, candidates.Count)], newPosition, Quaternion.identity);
                r.SetEntrance(Entrance.LEFT);
            }
        }
    }

    private void GenerateBottomRoom(LevelGenerator lg)
    {
        if (!blockedBottom)
        {
            if (roomType == RoomType.V || roomType == RoomType.HV)
            {
                Vector3 newPosition = new Vector3(transform.position.x, transform.position.y - roomHeight, transform.position.z);
                // Generate V or VH
                List<Room> candidates = new List<Room>();
                candidates.AddRange(GetExtendCandidates(RoomType.V, Entrance.TOP, lg));
                candidates.AddRange(GetExtendCandidates(RoomType.VH, Entrance.TOP, lg));
                Room r = Instantiate(candidates[Random.Range(0, candidates.Count)], newPosition, Quaternion.identity);
                r.SetEntrance(Entrance.TOP);
            }
        }
    }

    public RoomType GetRoomType()
    {
        return roomType;
    }

    private List<Room> GetExtendCandidates(RoomType type, Entrance entrance, LevelGenerator lg)
    {
        List<Room> candidates = new List<Room>();
        switch (type)
        {
            case RoomType.H:
                foreach (Room r in lg.h_rooms)
                {
                    if (entrance == Entrance.LEFT)
                    {
                        bool ok = false;
                        foreach (Vector2 IN in r.leftGates)
                        {
                            if (ok)
                            {
                                break;
                            }
                            foreach (Vector2 OUT in rightGates)
                            {
                                if (DetectPass(IN, OUT))
                                {
                                    candidates.Add(r);
                                    ok = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (entrance == Entrance.RIGHT)
                    {
                        bool ok = false;
                        foreach (Vector2 IN in r.rightGates)
                        {
                            if (ok)
                            {
                                break;
                            }
                            foreach (Vector2 OUT in leftGates)
                            {
                                if (DetectPass(IN, OUT))
                                {
                                    candidates.Add(r);
                                    ok = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
            case RoomType.HV:
                foreach (Room r in lg.hv_rooms)
                {
                    if (entrance == Entrance.LEFT)
                    {
                        bool ok = false;
                        foreach (Vector2 IN in r.leftGates)
                        {
                            if (ok)
                            {
                                break;
                            }
                            foreach (Vector2 OUT in rightGates)
                            {
                                if (DetectPass(IN, OUT))
                                {
                                    candidates.Add(r);
                                    ok = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (entrance == Entrance.RIGHT)
                    {
                        bool ok = false;
                        foreach (Vector2 IN in r.rightGates)
                        {
                            if (ok)
                            {
                                break;
                            }
                            foreach (Vector2 OUT in leftGates)
                            {
                                if (DetectPass(IN, OUT))
                                {
                                    candidates.Add(r);
                                    ok = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
            case RoomType.V:
                candidates.AddRange(lg.v_rooms);
                break;
            case RoomType.VH:
                candidates.AddRange(lg.vh_rooms);
                break;
        }

        return candidates;
    }

    private bool DetectPass(Vector2 a, Vector2 b)
    {
        if(a.y - b.x  >= 0f && b.y - a.x >= 0f)
        {
            return true;
        }
        return false;
    }
}
