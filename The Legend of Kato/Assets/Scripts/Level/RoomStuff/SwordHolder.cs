using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

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

    public void LightOff()
    {
        GetComponent<LightBlink>().enabled = false;
        GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
    }
}
