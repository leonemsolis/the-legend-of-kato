using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{
    Rigidbody2D rb;
    bool active = false;
    float activeDelay = .05f;

    [SerializeField] AudioClip pickUpSound;
    Health health;
    Collider2D _collider;
    bool pickedup = false;

    private void Start()
    {
        health = FindObjectOfType<Health>();
        _collider = GetComponent<Collider2D>();
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0f, Random.Range(10f, 17f)));
    }

    private void Update()
    {
        _collider.enabled = health.GetCurrentHealth() != Health.MAX_HEALTH;


        if (activeDelay > 0)
        {
            activeDelay -= Time.deltaTime;
        }
        else if (!active)
        {
            active = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.tag == "Player" || collision.tag == "Boots" || collision.tag == "Sword" || collision.tag == "Body")
            {
                if(!pickedup)
                {
                    if (FindObjectOfType<Health>().RestoreHealth())
                    {
                        FindObjectOfType<SoundPlayer>().PlaySound(pickUpSound, transform.position);
                        Destroy(transform.parent.gameObject);
                        pickedup = true;
                    }
                }
            }
        }
    }
}
