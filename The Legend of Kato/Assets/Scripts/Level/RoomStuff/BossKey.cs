using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKey : MonoBehaviour
{    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == C.PlayerTag)
        {
            FindObjectOfType<BossGate>().Open();
            Destroy(gameObject);
        }
    }
}
