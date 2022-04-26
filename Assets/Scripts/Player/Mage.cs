using UnityEngine;
using Mirror;

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
        
        if (attackAxis.magnitude <=0.1f) return;
        if (Bullet.time > 0) return;
        if (mp.ManaUsage(activeBul.manaCost))
        {
            CmdSpawnBullet();
        }
    }

    [Command]
    void CmdSpawnBullet()
    {
        Bullet bul = NetworkManager.Instantiate(activeBul, bulPoint.position, Quaternion.identity);
        bul.Shot(mageSprite.up);
        NetworkServer.Spawn(bul.gameObject, connectionToClient);
    }

    //привязать к игроку при подключении
    public void Dash()
    {
        if (!dl.RemoveCharge()) return;
        Vector2 dashPos = transform.position;
        dashPos += motionAxis.normalized * dashDistance;
        if (Physics2D.OverlapCircle(dashPos, 1.5f, collMask))
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