using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPObjectTest : HPObject
{
    GameManager gm;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    protected override void Delete()
    {
        gm.StartRevive(gameObject, 3);
    }
}