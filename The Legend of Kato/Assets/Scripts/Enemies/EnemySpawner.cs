using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject shark;
    Camera cam;
    const float minSpawnDelay = .3f;
    const float maxSpawnDelay = 1f;
    float timer = 2f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        TickTimer();
    }

    private void TickTimer()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Vector2 upper = cam.transform.position - Vector3.up * (cam.orthographicSize - CameraFollow.unitsToBottom - 1f);
            Vector2 left = upper + Vector2.left * 4f;
            Vector2 right = upper + Vector2.right * 4f;
            Vector2 direction = Vector2.down;
            float checkDistance = 4f;

            LayerMask collisionMask = 1 << LayerMask.NameToLayer("Ground");

            if (Random.Range(0f, 1f) > .5f)
            {
                if (Physics2D.Raycast(left, direction, checkDistance, collisionMask))
                {
                    SpawnEnemyAt(left);
                }
                else if (Physics2D.Raycast(right, direction, checkDistance, collisionMask))
                {
                    SpawnEnemyAt(right);
                }
            }
            else
            {
                if (Physics2D.Raycast(right, direction, checkDistance, collisionMask))
                {
                    SpawnEnemyAt(right);
                }
                else if (Physics2D.Raycast(left, direction, checkDistance, collisionMask))
                {
                    SpawnEnemyAt(left);
                }
            }

            RestartTimer();
        }
    }

    private void RestartTimer()
    {
        timer = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void SpawnEnemyAt(Vector2 sp)
    {
        Vector3 spawnPoint = new Vector3(sp.x, sp.y, 0f);
        Instantiate(shark, spawnPoint, Quaternion.identity);
    }
}
