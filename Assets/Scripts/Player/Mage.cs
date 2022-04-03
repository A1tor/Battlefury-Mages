using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mage : MageBase
{
    [Header("Dash:")]
    [SerializeField] float dashDistance;
    [SerializeField] LayerMask collMask;
    

    [Header("Attack:")]
    [SerializeField] Transform bulPoint;
    [SerializeField] Bolt boltPref;
    [SerializeField] Fire firePref;
    Bullet activeBul;

    protected override void Awake()
    {
        base.Awake();
        activeBul = firePref; 
    }
    protected override void Attack(Vector2 dir)
    {
        
        if (attackAxis.magnitude == 0) return;
        if (Bullet.time > 0) return;
        if (mp.ManaUsage(firePref.manaCost))
        {
            Bullet bul = Instantiate(activeBul, bulPoint.position, Quaternion.identity);
            bul.Shot(attackAxis);
        }
    }
    //привязать к игроку при подключении
    public void Dash()
    {
        if (!dl.RemoveCharge()) return;
        Vector2 dashPos = transform.position;
        dashPos += motionAxis.normalized * dashDistance;
        if (Physics2D.OverlapCircle(dashPos, 1, collMask))
        {
            RaycastHit2D hit = Physics2D.Raycast(bulPoint.position, motionAxis);
            print(hit.collider.name);
            dashPos = transform.position;
            dashPos+= motionAxis.normalized * (hit.distance-1);
        }

        transform.position = dashPos;
        mageSprite.rotation = Quaternion.LookRotation(Vector3.forward, motionAxis);
    }

    public override void OnUltStart()
    {
        base.OnUltStart();
        activeBul = boltPref;

    }
    public override void OnUltEnd()
    {
        base.OnUltEnd();
        activeBul = firePref;

    }
}