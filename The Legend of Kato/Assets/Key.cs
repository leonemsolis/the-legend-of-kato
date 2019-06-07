using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject doorSilhouette;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sword" || collision.tag == "Player")
        {
            // Create door silhouette on the door object (1st child of the current parent)
            Instantiate(doorSilhouette, transform.parent.GetChild(0).transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }
}
