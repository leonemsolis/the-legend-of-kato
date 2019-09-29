using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] AudioClip pickUpSound;
    [SerializeField] GameObject doorSilhouette;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == C.SwordTag || collision.tag == C.PlayerTag)
        {
            // Create door silhouette on the door object (1st child of the current parent)
            var dS = Instantiate(doorSilhouette, transform.parent.GetChild(0).transform.position, Quaternion.identity);
            // Assign room as parent
            dS.transform.parent = transform.parent.parent;
            FindObjectOfType<SoundPlayer>().PlaySound(pickUpSound, transform.position);
            Destroy(transform.parent.gameObject);
        }
    }
}
