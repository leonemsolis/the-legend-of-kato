using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] AudioClip shootSound;
    [SerializeField] bool shootingRight = false;
    [SerializeField] TurretProjectile projectile;

    Animator animator;

    const string IDLE_ANIM = "idle";
    const string ATTACK_ANIM = "attack";
    const float shootDelay = 2f;
    const float shootAnimTime = .5f;
    const float shootAnimDelay = shootDelay - shootAnimTime;

    Vector3 shootOffset;

    void Start()
    {
        animator = GetComponent<Animator>();
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

    private IEnumerator PreShootAnimStart()
    {
        yield return new WaitForSeconds(shootAnimDelay);
        animator.Play(ATTACK_ANIM);
        StartCoroutine(StopShootAnim());

    }

    private IEnumerator StopShootAnim()
    {
        yield return new WaitForSeconds(shootAnimTime);
        animator.Play(IDLE_ANIM);
    }

    private IEnumerator Shoot()
    {
        StartCoroutine(PreShootAnimStart());
        yield return new WaitForSeconds(shootDelay);
        FindObjectOfType<SoundPlayer>().PlaySimultaniousSound(shootSound, transform.position);
        TurretProjectile p = Instantiate(projectile, transform.position + shootOffset, Quaternion.identity);
        p.transform.parent = transform;
        p.StartMoving(shootingRight);
        StartCoroutine(Shoot());
    }
}
