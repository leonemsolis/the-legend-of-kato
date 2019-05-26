using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seaweed : MonoBehaviour
{
	[SerializeField] EnemyHitBox myHitBox;

	void Start()
    {
        myHitBox = Instantiate(myHitBox);
        myHitBox.SetEnemy(gameObject.transform, new Vector2(0f, -0.1229651f), new Vector2(0.6281383f, 0.7440698f));
    }
}
