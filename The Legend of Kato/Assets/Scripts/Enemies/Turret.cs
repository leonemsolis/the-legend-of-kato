using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] AudioClip shootSound;
    [SerializeField] bool shootingRight = false;
    [SerializeField] TurretProjectile projectile;
    const float shootDelay = 2f;
    Vector3 shootOffset;

    void Start()
    {
        // Default sprite turned left
        if(shootingRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        shootOffset = shootingRight ? new Vector3(40f, 17.5f, 0f) : new Vector3(-40f, 17.5f, 0f);
    }

    private void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);
        FindObjectOfType<SoundPlayer>().PlaySound(shootSound, transform.position);
        TurretProjectile p = Instantiate(projectile, transform.position + shootOffset, Quaternion.identity);
        p.transform.parent = transform;
        p.StartMoving(shootingRight);
        StartCoroutine(Shoot());
    }
}
