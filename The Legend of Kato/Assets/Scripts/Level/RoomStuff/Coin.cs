﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    bool active = false;
    float activeDelay = .05f;

    [SerializeField] AudioClip pickUpSound;

    private void Start()
    {
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0f, Random.Range(10f, 17f)));
    }

    private void Update()
    {
        if (activeDelay > 0)
        {
            activeDelay -= Time.deltaTime;
        }
        else if(!active)
        {
            active = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active)
        {
            if (collision.tag == "Player" || collision.tag == "Boots" || collision.tag == "Sword" || collision.tag == "Body")
            {
                Destroy(transform.parent.gameObject);
                FindObjectOfType<SoundPlayer>().PlaySound(pickUpSound, transform.position);
                FindObjectOfType<ScoreBoardText>().IncreaseScore(1);
            }
        }
    }
}
