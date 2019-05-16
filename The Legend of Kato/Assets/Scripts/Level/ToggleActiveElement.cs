using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActiveElement : MonoBehaviour
{
    [SerializeField] bool movingObject = false;

    Camera cam;
    SpriteRenderer spriteRenderer;
    Collider2D collider2d;
    Rigidbody2D rb;

    float deltaX, deltaY;

    void Start()
    {
        cam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        deltaX = 5.5f;
        deltaY = cam.orthographicSize + .5f;

        if(!movingObject)
        {
            deltaX+=1.5f;
            deltaY+=1.5f;
        }

        if (GetComponent<BoxCollider2D>() != null)
        {
            collider2d = GetComponent<BoxCollider2D>();
        }
        if(GetComponent<CapsuleCollider2D>() != null)
        {
            collider2d = GetComponent<CapsuleCollider2D>();
        }
    }

    private bool IsFar()
    {
        if((Mathf.Abs(transform.position.x - cam.transform.position.x) > deltaX) ||
            (Mathf.Abs(transform.position.y - cam.transform.position.y) > deltaY))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void FixedUpdate()
    {
        if(IsFar())
        {
            spriteRenderer.enabled = false;
            collider2d.enabled = false;
            rb.simulated = false;
        }
        else
        {
            spriteRenderer.enabled = true;
            collider2d.enabled = true;
            rb.simulated = true;
        }
    }
}
