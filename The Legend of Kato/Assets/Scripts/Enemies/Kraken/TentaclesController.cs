using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesController : MonoBehaviour
{

    [SerializeField] TentacleController tentacle;
    Vector2[] spawnPoints;
    KrakenSigns krakenSigns;
    TentacleController currentTentacle;
    PlayerController player;

    int selectedPosition;
    bool selectedSideLeft;

    PlayerRoomDetector roomDetector;


    int lives = 5;

    float enterTime = 3f;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        krakenSigns = FindObjectOfType<KrakenSigns>();

        spawnPoints = new Vector2[4];
        spawnPoints[0] = new Vector2(-349f, -570f);
        spawnPoints[1] = new Vector2(-99f, -570f);
        spawnPoints[2] = new Vector2(149f, -570f);
        spawnPoints[3] = new Vector2(402f, -570f);

        roomDetector = FindObjectOfType<PlayerRoomDetector>();
    }


    void Update()
    {
        if(roomDetector.InTheRoom() && roomDetector.GetCurrentRoom().RoomID == C.RoomIDBoss)
        {
            if(enterTime > 0)
            {
                enterTime -= Time.deltaTime;
            }
            else
            {
                if (currentTentacle == null)
                {
                    if (lives != 0)
                    {
                        SetPositionAndSide();
                        currentTentacle = Instantiate(tentacle, Vector2.zero, Quaternion.identity);
                        currentTentacle.gameObject.transform.SetParent(transform);
                        currentTentacle.transform.localPosition = spawnPoints[selectedPosition];
                        currentTentacle.gameObject.transform.localScale = new Vector3(selectedSideLeft ? 1f : -1f, 1f, 0f);

                        krakenSigns.Enable(selectedPosition, selectedSideLeft);
                    }
                    else
                    {
                        //DEAD
                        FindObjectOfType<KrakenController>().Defeat();
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    public void TakeDamage()
    {
        if(lives > 0)
        {
            lives--;
        }
    }

    private void SetPositionAndSide()
    {
        float deltaPlayerX = player.transform.position.x - transform.position.x;
        if (deltaPlayerX <= -406.7236f) // 1st platform
        {
            selectedPosition = Random.Range(0, 2);
            selectedSideLeft = true;
        } 
        else if(deltaPlayerX > -406.7236f && deltaPlayerX <= -148.0226f) // 2nd platform
        {
            selectedPosition = Random.Range(0, 3);
            if(selectedPosition < 1)
            {
                selectedSideLeft = false;
            }
            else
            {
                selectedSideLeft = true;
            }
        } 
        else if(deltaPlayerX > -148.0226f && deltaPlayerX <= 86.02466) // 3nd platform
        {
            selectedPosition = Random.Range(0, 4);
            if(selectedPosition < 2)
            {
                selectedSideLeft = false;
            }
            else
            {
                selectedSideLeft = true;
            }
        } 
        else if(deltaPlayerX > 86.02466 && deltaPlayerX <= 404.3813) // 4nd platform
        {
            selectedPosition = Random.Range(1, 4);
            if (selectedPosition < 3)
            {
                selectedSideLeft = false;
            }
            else
            {
                selectedSideLeft = true;
            }
        } 
        else if(deltaPlayerX > 404.3813) // 5nd platform
        {
            selectedPosition = Random.Range(2, 4);
            selectedSideLeft = false;
        }
    }
}
