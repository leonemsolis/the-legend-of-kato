using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleFishSpawner : MonoBehaviour
{
    [SerializeField] ParticleSystem littleFish;
    const float maxSpawnDelay = 10f;
    const float minSpawnDelay = 5f;

    void Start()
    {
        StartCoroutine(SpawnFishes());
    }

    private IEnumerator SpawnFishes()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        ParticleSystem s = Instantiate(littleFish, transform.position + new Vector3(600f, 0f, 0f), Quaternion.identity);
        s.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        StartCoroutine(SpawnFishes());
    }
}
