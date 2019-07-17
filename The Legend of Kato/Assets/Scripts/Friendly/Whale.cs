using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : MonoBehaviour
{
    [SerializeField] Shop shop;
    PlayerController player;
    Shop instantiatedShop;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == C.PlayerTag)
        {
            player.CanMove = false;
            instantiatedShop = Instantiate(shop, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f), Quaternion.identity);

            // Change all buttons mode
            foreach(ButtonMode b in FindObjectsOfType<ButtonMode>())
            {
                b.ChangeMode(Mode.SHOP);
            }
        }
    }

    public void LeaveWhale()
    {
        player.CanMove = true;
        // Change all buttons mode
        foreach (ButtonMode b in FindObjectsOfType<ButtonMode>())
        {
            b.ChangeMode(Mode.DEFAULT);
        }
        Destroy(instantiatedShop.gameObject);
        Destroy(gameObject);
    }

}
