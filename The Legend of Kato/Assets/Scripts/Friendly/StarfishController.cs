using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishController : MonoBehaviour
{
    [SerializeField] GameObject healPackPrefab;
    [SerializeField] GameObject smokePrefab;
    [SerializeField] AudioClip hitSound;

    bool active = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == C.PlayerTag && active)
        {
            active = false;
            Instantiate(healPackPrefab, transform.position, Quaternion.identity);
            Instantiate(smokePrefab, transform.position, Quaternion.identity);
            FindObjectOfType<SoundPlayer>().PlaySound(hitSound, transform.position);

            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, hitSound.length + 1f);
        }
    }
}
