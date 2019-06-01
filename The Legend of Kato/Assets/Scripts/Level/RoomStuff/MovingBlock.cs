using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [Header("Block is moving to the left, or to the bottom")]
    [SerializeField] float moveDistance = 300f;
    [SerializeField] bool horizontal = true;

    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float firstTimeDelay = 2f;
    Vector3 origin;
    bool movingTowards = true;
    Vector3 destination;
    private void Start()
    {
        origin = transform.position;

        if(horizontal)
        {
            destination = new Vector3(origin.x - moveDistance, origin.y, origin.z);
        }
        else
        {
            destination = new Vector3(origin.x, origin.y - moveDistance, origin.z);
        }
    }

    void Update()
    {
        if(firstTimeDelay > 0)
        {
            firstTimeDelay -= Time.deltaTime;
        }
        else
        {
            if (movingTowards)
            {
                if (Vector3.Distance(origin, transform.position) > moveDistance)
                {
                    movingTowards = false;
                    transform.position = destination;
                }
                else
                {
                    if (horizontal)
                    {
                        transform.position = new Vector3(transform.position.x - movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed * Time.deltaTime, transform.position.z);
                    }
                }
            }
            else
            {
                if (Vector3.Distance(origin, transform.position) < 5f)
                {
                    movingTowards = true;
                    transform.position = origin;
                }
                else
                {
                    if (horizontal)
                    {
                        transform.position = new Vector3(transform.position.x + movementSpeed * Time.deltaTime, transform.position.y, transform.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed * Time.deltaTime, transform.position.z);
                    }
                }
            }
        }
    }
}
