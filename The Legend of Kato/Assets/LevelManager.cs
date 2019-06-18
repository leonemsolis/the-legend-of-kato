using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Each inner list contains rooms that on the "same" y-axis
    [SerializeField] List<List<GameObject>> rooms;
    // Each element is a hall that separates pair of rooms
    [SerializeField] List<GameObject> halls;

    void Update()
    {
        // When Character enters certain room, remove all rooms & halls that are on the left
        // + Also block doors on the same y-axis rooms
        //
        // When Character leaves certain room??
    }
}
