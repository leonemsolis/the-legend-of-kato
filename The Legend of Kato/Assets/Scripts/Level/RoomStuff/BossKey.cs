using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKey : MonoBehaviour
{
    [SerializeField] AudioClip pickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == C.PlayerTag)
        {
            FindObjectOfType<SoundPlayer>().PlaySound(pickUpSound, transform.position);
            FindObjectOfType<BossGate>().Open();
            Destroy(gameObject);
        }
    }
}
