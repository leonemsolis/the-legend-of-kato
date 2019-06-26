using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishSpawner : MonoBehaviour
{
    [SerializeField] Jellyfish jellyfish;
    [SerializeField] bool shootingRight = false;
    const float shootDelay = 1.6f;

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(shootDelay);
        Jellyfish j = Instantiate(jellyfish, transform.position, Quaternion.identity);
        j.transform.parent = transform;
        j.StartMovement(shootingRight);
        StartCoroutine(Spawn());
    }
}
