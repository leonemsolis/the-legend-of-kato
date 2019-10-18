using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem bubble;
    const float maxSpawnDelay = 3f;
    const float minSpawnDelay = 1f;
    const float maxY = 600f;
    const float minY = -300f;
    const float maxXDelta = 450f;

    void Start()
    {
        StartCoroutine(SpawnBubble());
        StartCoroutine(SpawnBubble());
        StartCoroutine(SpawnBubble());
    }

    private IEnumerator SpawnBubble()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        Instantiate(bubble, transform.position + new Vector3(Random.Range(-maxXDelta, maxXDelta), Random.Range(minY, maxY), 0f), Quaternion.identity);
        StartCoroutine(SpawnBubble());
    }
}
