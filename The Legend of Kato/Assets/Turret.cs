using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] bool shootingRight = false;
    [SerializeField] TurretProjectile projectile;
    const float shootDelay = 2f;

    void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);
        TurretProjectile p = Instantiate(projectile, transform.position, Quaternion.identity);
        p.StartMoving(shootingRight);
        StartCoroutine(Shoot());
    }
}
