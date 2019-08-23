using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenController : MonoBehaviour
{

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
