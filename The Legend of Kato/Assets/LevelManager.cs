using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Each object is set of rooms that are on the same "x-coordinate"
    [SerializeField] List<GameObject> sets;
    // Transitions between rooms
    [SerializeField] List<GameObject> transitions;

    PlayerRoomDetector detector;
    int lastSetIndex = -1;
    bool locked = false;

    private void Start()
    {
        detector = FindObjectOfType<PlayerRoomDetector>();

        // Disable all rooms and transitions except first
        for(int i = 1; i < sets.Count; ++i)
        {
            // Number of transitions less that number of rooms
            if(i != sets.Count - 1)
            {
                transitions[i].SetActive(false);
            }
            sets[i].SetActive(false);
        }
    }

    void Update()
    {
        if(detector.InTheRoom())
        {
            if(!locked)
            {
                locked = true;
                // Remove back rooms & transitions
                int currentSetIndex = -1;
                for (int i = 0; i < sets.Count; ++i)
                {
                    for (int j = 0; j < sets[i].transform.childCount; ++j)
                    {
                        Room r = sets[i].transform.GetChild(j).GetComponent<Room>();
                        if (detector.GetCurrentRoom().RoomID == r.RoomID)
                        {
                            currentSetIndex = i;
                            break;
                        }
                    }
                    if (currentSetIndex != -1)
                    {
                        break;
                    }
                }
                lastSetIndex = currentSetIndex;

                for (int i = 0; i < currentSetIndex; ++i)
                {
                    sets[i].SetActive(false);
                    transitions[i].SetActive(false);
                }
            }
        }
        if(!detector.InTheRoom())
        {
            if(locked)
            {
                locked = false;
                // Enable 1 further rooms & transitions
                sets[lastSetIndex + 1].SetActive(true);
                // Number of transitions less that number of rooms
                if (lastSetIndex + 1 < transitions.Count)
                {
                    transitions[lastSetIndex + 1].SetActive(true);
                }
            }
        } 
    }
}
