using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchProjectile : MonoBehaviour
{
    Vector2 origin;
    Vector3 velocity;
    const float maxDelta = 1500f;
    const float speed = 200f;

    void Start()
    {
        origin = transform.position;
        Vector3 destination = FindObjectOfType<PlayerController>().transform.position;

        float deltaX = origin.x - destination.x;
        float deltaY = origin.y - destination.y;
        float hypotenuse = Mathf.Sqrt(Mathf.Pow(deltaX, 2f) + Mathf.Pow(deltaY, 2f));

        float coefficient = speed / hypotenuse;

        velocity = new Vector3(deltaX * coefficient, deltaY * coefficient, 0f);
    }


    void Update()
    {
        transform.position = transform.position - velocity * Time.deltaTime;
        if(Vector3.Distance(origin, transform.position) > maxDelta)
        {
            Destroy(gameObject);
        }
    }
}
