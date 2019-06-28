using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    [SerializeField] GameObject shop;
    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == C.PlayerTag)
        {
            player.CanMove = false;
            Instantiate(shop, transform.position + new Vector3(0f, 250f, 0f), Quaternion.identity);

            // Change all buttons mode
            foreach(ButtonMode b in FindObjectsOfType<ButtonMode>())
            {
                b.ChangeMode(Mode.SHOP);
            }
        }
    }
}
