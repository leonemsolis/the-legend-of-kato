#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject shark;
    SpriteRenderer spriteRenderer;
    bool spawning = false;
    const float spawnDelay = 1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(NeedToSpawn())
        {
            SpawnEnemy();
        }
    }

    private bool NeedToSpawn()
    {
        return transform.childCount == 0 && !spawning;
    }

    private void SpawnEnemy()
    {
        spriteRenderer.enabled = true;
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        spawning = true;
        yield return new WaitForSeconds(spawnDelay);
        GameObject g = Instantiate(shark, transform.position, Quaternion.identity);
        g.transform.parent = transform;
        spawning = false;
        spriteRenderer.enabled = false;
    }
}
