using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHolder : MonoBehaviour
{
    bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!pickedUp)
        {
            if (collision.tag == "Player")
            {
                pickedUp = true;
                GetComponent<Animator>().Play("SwordHolder_EMPTY");
                FindObjectOfType<PlayerAnimator>().StartPickupSword();
            }
        }
    }
}
