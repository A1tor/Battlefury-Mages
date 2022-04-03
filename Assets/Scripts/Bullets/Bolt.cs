using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Bullet
{
    [SerializeField] float maxSpeed;
    private int savedDamage;
    [SerializeField] float CD;
    private void Start()
    {
        savedDamage = damage;
        damage = 0;
    }
    public override void Shot(Vector2 dir)
    {
        base.Shot(dir);
        time = CD;
    }
    public void Accel()
    {
        rb.velocity = transform.right * maxSpeed;
        damage = savedDamage;
    }
}