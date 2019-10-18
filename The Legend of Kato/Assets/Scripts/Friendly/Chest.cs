using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] Sprite openSprite;
    [SerializeField] GameObject soulPrefab;
    [SerializeField] GameObject hpPrefab;
    [SerializeField] AudioClip chestOpenAudio;

    bool closed = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(closed)
        {
            if (collision.tag == "Player" || collision.tag == "Boots" || collision.tag == "Sword" || collision.tag == "Body")
            {
                FindObjectOfType<SoundPlayer>().PlaySound(chestOpenAudio, transform.position);
                GetComponent<SpriteRenderer>().sprite = openSprite;


                for(int i = 0; i < Random.Range(1, 3); ++i)
                {
                    Instantiate(soulPrefab, transform.position + new Vector3(Random.Range(-100f, 100f), 0f, 0f), Quaternion.identity);
                }
                for(int i = 0; i < Random.Range(1, 2); ++i)
                {
                    Instantiate(hpPrefab, transform.position + new Vector3(Random.Range(-100f, 100f), 0f, 0f), Quaternion.identity);
                }
                closed = false;
            }
        }
    }
}
