using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandom : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    void Update()
    {
        
    }
}
