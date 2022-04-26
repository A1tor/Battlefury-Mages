using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] disComps;
    [SerializeField] GameObject actObjs;

    Rigidbody2D rb;

    void Start()
    {
        if(isLocalPlayer)
        {
            actObjs.SetActive(true);
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb) rb.bodyType = RigidbodyType2D.Static;
        foreach(Behaviour comp in disComps)
        {
            comp.enabled = false;
        }
    }

   
    void Update()
    {
        
    }
}
