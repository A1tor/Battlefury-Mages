using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class __NetManager__ : NetworkManager
{
    [Header("////////Spawn Objects\\\\\\\\\\\\\\\\\\\\\\\\")]
    [SerializeField]  GameManager gmPref;
    [SerializeField] GameObject boxPref;
    [SerializeField] Transform[] boxSpawns;
    public override void OnStartServer()
    {
        NetworkServer.Spawn(Instantiate(gmPref).gameObject);

        foreach(Transform point in boxSpawns)
        {
            NetworkServer.Spawn(Instantiate(boxPref, point.position,Quaternion.identity));
        }
    }

}
