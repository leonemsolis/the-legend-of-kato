using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalicationSpriteChanger : MonoBehaviour
{
    [SerializeField] int id;
    
    private void OnEnable() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = FindObjectOfType<SpriteTranslator>().GetSprite(spriteRenderer.sprite, id);
    }
}
