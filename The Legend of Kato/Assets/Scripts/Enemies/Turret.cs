using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Sprite left;
    [SerializeField] Sprite right;
    [SerializeField] bool shootingRight = false;
    [SerializeField] TurretProjectile projectile;
    const float shootDelay = 2f;
    Vector3 shootOffset;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = shootingRight ? right : left;
        shootOffset = shootingRight ? new Vector3(40f, 17.5f, 0f) : new Vector3(-40f, 17.5f, 0f);
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);
        TurretProjectile p = Instantiate(projectile, transform.position + shootOffset, Quaternion.identity);
        p.StartMoving(shootingRight);
        StartCoroutine(Shoot());
    }
}
