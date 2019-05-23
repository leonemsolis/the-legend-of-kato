using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject shark;
    SpriteRenderer spriteRenderer;
    bool spawning = false;

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
        yield return new WaitForSeconds(2f);
        GameObject g = Instantiate(shark, transform.position, Quaternion.identity);
        g.transform.parent = transform;
        spawning = false;
        spriteRenderer.enabled = false;
    }
}
