using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject shark;

    PlayerController player;
    float lowestY;
    const float generatorThreshold = 15f;
    const float leftWallX = 0f;
    const float rightWallX = 10f;

    //TODO: delete
    bool left = false;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        UpdateLowestY();
    }

    private void UpdateLowestY()
    {
        float lowest = 100f;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Wall"))
        {
            if(g.transform.position.y < lowest)
            {
                lowest = g.transform.position.y;
            }
        }
        lowestY = lowest;
    }

    void Update()
    {
        if(Mathf.Abs(lowestY - player.gameObject.transform.position.y) < generatorThreshold)
        {
            Generate();
        }
    }

    private void Generate()
    {
        for(int i = 0; i < 5; ++i)
        {
            Instantiate(wall, new Vector3(leftWallX, lowestY - 1f- i, 0f), Quaternion.identity);
            Instantiate(wall, new Vector3(rightWallX, lowestY - 1f - i, 0f), Quaternion.identity);
        }

        UpdateLowestY();

        if (left)
        {
            for (int i = 0; i < 6; ++i)
            {
                Instantiate(platform, new Vector3(leftWallX + 1f + i, lowestY, 0f), Quaternion.identity);
            }
            Instantiate(shark, new Vector3(leftWallX + 2f, lowestY + 2f, 0f), Quaternion.identity);
        }
        else
        {
            for (int i = 0; i < 6; ++i)
            {
                Instantiate(platform, new Vector3(rightWallX - 1f - i, lowestY, 0f), Quaternion.identity);
            }
            Instantiate(shark, new Vector3(rightWallX - 2f, lowestY + 2f, 0f), Quaternion.identity);
        }



        left = !left;
    }
}
