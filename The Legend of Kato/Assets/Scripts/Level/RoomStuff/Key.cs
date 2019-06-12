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
            var dS = Instantiate(doorSilhouette, transform.parent.GetChild(0).transform.position, Quaternion.identity);
            // Assign room as parent
            dS.transform.parent = transform.parent.parent;
            Destroy(transform.parent.gameObject);
        }
    }
}
