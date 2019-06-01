using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusController : MonoBehaviour
{
    [Header("Octupus is moving down then up")]

    [SerializeField] EnemyHitBox myHitBox;
    [SerializeField] float movingDistance = 300f;
    [SerializeField] float firstTimeDelay = 0f;
    [SerializeField] float movingSpeed = 100f;
    bool movingDown = true;
    Vector3 origin;
    Vector3 destination;

    void Start()
    {
        myHitBox = Instantiate(myHitBox);
        myHitBox.SetEnemy(gameObject.transform, new Vector2(0f, 0f), new Vector2(1f, 1f));

        origin = transform.position;
        destination = transform.position - new Vector3(0f, movingDistance, 0f);
    }

    void Update()
    {
        if(firstTimeDelay > 0)
        {
            firstTimeDelay -= Time.deltaTime;
        }
        else
        {
            if (movingDown)
            {
                if (Vector3.Distance(origin, transform.position) > movingDistance)
                {
                    movingDown = false;
                    transform.position = destination;
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - movingSpeed * Time.deltaTime, transform.position.z);
                }
            }
            else
            {
                if (Vector3.Distance(origin, transform.position) < 5f)
                {
                    movingDown = true;
                    transform.position = origin;
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + movingSpeed * Time.deltaTime, transform.position.z);
                }
            }
        }
    }
}
