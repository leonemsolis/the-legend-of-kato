using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Transform player;
    [SerializeField] float dampTime = .15f;
    [SerializeField] float velocityY = 100f;
    Vector3 velocity;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;
        velocity = new Vector3(0f, velocityY, 0f);
    }

    private void Update()
    {
        Vector3 point = Camera.main.WorldToViewportPoint(player.position);
        Vector3 delta = player.position - Camera.main.ViewportToWorldPoint(new Vector3(point.x, .7f, point.z)); 
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
