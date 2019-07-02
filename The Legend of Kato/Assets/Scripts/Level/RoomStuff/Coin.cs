using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    bool active = false;
    float activeDelay = .1f;

    private void Start()
    {
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0f, 25f));
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
                FindObjectOfType<ScoreBoardText>().IncreaseScore(1);
            }
        }
    }
}
