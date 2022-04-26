using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Bullet
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer sr;

    public override void Awake()
    {
        base.Awake();
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
        sr.flipX = Random.Range(0, 2) == 0;
    }
    private void FixedUpdate()
    {
        sr.transform.localScale += new Vector3(0.05f, 0.05f,0);
        sr.color = sr.color - new Color(0, 0, 0, 0.015f);
    }
}