using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        // Randomize side
        if(Random.Range(0, 2) == 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
