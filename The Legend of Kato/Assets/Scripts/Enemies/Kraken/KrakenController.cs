using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenController : MonoBehaviour
{
    [SerializeField] AudioClip dissapearSound;
    Animator animator;
    bool defeated = false;
    float defeatTimer = 4.7f;

    BossGate bossGate;

    void Start()
    {
        bossGate = FindObjectOfType<BossGate>();
        animator = GetComponent<Animator>();
        bossGate.gameObject.SetActive(false);
    }

    public void Defeat()
    {
        animator.Play("KrakenDefeat");
        defeated = true;
        StartCoroutine(disappearSoundPlayer());
    }

    IEnumerator disappearSoundPlayer()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<SoundPlayer>().PlaySound(dissapearSound);
        StartCoroutine(disappearSoundPlayer());
    }

    private void Update()
    {
        if(defeated)
        {
            if(defeatTimer > 0)
            {
                defeatTimer -= Time.deltaTime;
            }
            else
            {
                bossGate.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
