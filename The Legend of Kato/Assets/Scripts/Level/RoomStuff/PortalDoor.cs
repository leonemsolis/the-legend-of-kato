using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoor : MonoBehaviour
{
    [SerializeField] bool doorA = true;
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        Vector2 origin = transform.position + new Vector3(0f, -25f, 0f);
        float distance = 30f;
        LayerMask collisionMask = 1 << LayerMask.NameToLayer("Sword");
        RaycastHit2D hitLeft = Physics2D.Raycast(origin, Vector2.left, distance, collisionMask);
        RaycastHit2D hitRight = Physics2D.Raycast(origin, Vector2.right, distance, collisionMask);

        if(hitLeft && player.IsFacingRight())
        {
            transform.parent.gameObject.GetComponent<Portal>().Teleport(!doorA, true);
        } else if(hitRight && !player.IsFacingRight())
        {
            transform.parent.gameObject.GetComponent<Portal>().Teleport(!doorA, false);
        }
    }
}
