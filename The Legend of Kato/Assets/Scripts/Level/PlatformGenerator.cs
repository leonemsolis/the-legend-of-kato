using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] GameObject halfBlock;
    [SerializeField] GameObject block;
    [SerializeField] GameObject lava;
    List<GameObject> currentPlatform;
    List<GameObject> nextPlatform;
    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        currentPlatform = new List<GameObject>();
        nextPlatform = new List<GameObject>();
    }

    public Vector2 GeneratePlatforms(GameObject exitPlatform)
    {
        nextPlatform.Clear();
        nextPlatform.Add(exitPlatform);

        GenerateHallEntrance(exitPlatform.transform.position);

        for (int i = 0; i < 8; ++i)
        {
            currentPlatform = new List<GameObject>(nextPlatform);
            GenerateNextPlatform();
            GenerateHall(currentPlatform[0].transform.position, nextPlatform[0].transform.position);
        }

        GenerateHallExit(nextPlatform[0].transform.position, nextPlatform[nextPlatform.Count - 1].transform.position);

        // Return position of the last generated platform
        return nextPlatform[nextPlatform.Count - 1].transform.position;
    }

    private void GenerateHallEntrance(Vector2 currentPlatformStart)
    {
        for(int i = 1; i <= 6; ++i)
        {
            Instantiate(block, new Vector2(currentPlatformStart.x, currentPlatformStart.y - i * 100f), Quaternion.identity);
        }
    }

    private void GenerateHallExit(Vector2 nextPlatformStart, Vector2 nextPlatformEnd)
    {
        for (float i = nextPlatformStart.x + 100f; i <= nextPlatformEnd.x; i += 100f)
        {
            Instantiate(block, new Vector2(i, nextPlatformStart.y + 600f), Quaternion.identity);
            Instantiate(block, new Vector2(i, nextPlatformStart.y - 600f), Quaternion.identity);
            //Generate lava
            Instantiate(lava, new Vector2(i, nextPlatformStart.y - 500f), Quaternion.identity);
        }

        for (int i = 0; i < 3; ++i)
        {
            Instantiate(block, new Vector2(nextPlatformEnd.x, nextPlatformEnd.y + 500f - i * 100f), Quaternion.identity);
        }
    }

    private void GenerateHall(Vector2 currentPlatformStart, Vector2 nextPlatformStart)
    {
        for(float i = currentPlatformStart.x + 100f; i < nextPlatformStart.x; i+=100f)
        {
            Instantiate(block, new Vector2(i, currentPlatformStart.y + 600f), Quaternion.identity);
            Instantiate(block, new Vector2(i, currentPlatformStart.y - 600f), Quaternion.identity);
            //Generate lava on the bottom hall blocks
            Instantiate(lava, new Vector2(i, currentPlatformStart.y - 500f), Quaternion.identity);
        }

        //Generate block that closes lava on the end
        Instantiate(block, new Vector2(nextPlatformStart.x, currentPlatformStart.y - 500f), Quaternion.identity);

        for(float i = currentPlatformStart.y + 600f; i >= nextPlatformStart.y + 600f; i-=100f) 
        {
            Instantiate(block, new Vector2(nextPlatformStart.x, i), Quaternion.identity);
        }

        for (float i = currentPlatformStart.y - 600f; i >= nextPlatformStart.y - 600f; i -= 100f)
        {
            Instantiate(block, new Vector2(nextPlatformStart.x, i), Quaternion.identity);
        }

    }

    private void GenerateNextPlatform()
    {
        float anchorX = currentPlatform[0].transform.position.x;
        float anchorY = currentPlatform[0].transform.position.y;

        foreach (GameObject g in currentPlatform)
        {
            if (anchorX < g.transform.position.x)
            {
                anchorX = g.transform.position.x;
            }
        }

        nextPlatform.Clear();
        int p = Random.Range(0, 100);
        float firstBlockX = 0f;
        float firstBlockY = 0f;

        if (p >= 0 && p <= 19)
        {
            //0
            firstBlockY = anchorY;
            firstBlockX = anchorX + (int)Random.Range(3, 7) * 100f;
        }
        else if (p >= 20 && p <= 39)
        {
            //-1
            firstBlockY = anchorY - 100f;
            firstBlockX = anchorX + (int)Random.Range(2, 7) * 100f;
        }
        else if (p >= 40 && p <= 59)
        {
            //-2
            firstBlockY = anchorY - 200f;
            firstBlockX = anchorX + (int)Random.Range(2, 7) * 100f;
        }
        else if (p >= 60 && p <= 79)
        {
            //-3
            firstBlockY = anchorY - 300f;
            firstBlockX = anchorX + (int)Random.Range(2, 7) * 100f;
        }
        else if (p >= 80 && p <= 99)
        {
            //-4
            firstBlockY = anchorY - 400f;
            firstBlockX = anchorX + (int)Random.Range(2, 7) * 100f;
        }

        int blockCount = Random.Range(2, 5);
        for (int i = 0; i < blockCount; ++i)
        {
            GameObject newBlock = Instantiate(halfBlock, new Vector3(firstBlockX + i * 100f, firstBlockY, 0f), Quaternion.identity);
            nextPlatform.Add(newBlock);
        }
    }
}
