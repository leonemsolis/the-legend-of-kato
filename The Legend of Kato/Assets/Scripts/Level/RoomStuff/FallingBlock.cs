using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    [SerializeField] bool halfBlock = false;
    [SerializeField] float fallDelay = .5f;
    [SerializeField] const float restoreDelay = 4f;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    PlayerController player;
    bool active = true;
    float halfBlockYOffset = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>();
        if(halfBlock)
        {
            halfBlockYOffset = -50f;
        }
    }

    void Update()
    {
        Vector2 center = transform.position + new Vector3(0f, halfBlockYOffset, 0f);
        Vector2 left = transform.position + new Vector3(-50f, halfBlockYOffset, 0f);
        Vector2 right = transform.position + new Vector3(50f, halfBlockYOffset, 0f);

        Vector2 direction = Vector2.up;
        float distance = 60f;
        LayerMask collisionMask = 1 << LayerMask.NameToLayer("Player");
        RaycastHit2D hitPlayerLeft = Physics2D.Raycast(left, direction, distance, collisionMask);
        RaycastHit2D hitPlayerCenter = Physics2D.Raycast(center, direction, distance, collisionMask);
        RaycastHit2D hitPlayerRight = Physics2D.Raycast(right, direction, distance, collisionMask);

        if(hitPlayerLeft || hitPlayerCenter || hitPlayerRight)
        {
            if(active && player.transform.position.y > transform.position.y)
            {
                StartCoroutine(Toggle());
            }
        }
    }

    private IEnumerator Toggle()
    {
        if(active)
        {
            yield return new WaitForSeconds(fallDelay);
            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
            active = false;
            StartCoroutine(Toggle());
        }
        else
        {
            yield return new WaitForSeconds(restoreDelay);
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
            active = true;
        }
    }
}
