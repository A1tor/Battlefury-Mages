using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] float spread;
    [SerializeField] float destTime;
    [SerializeField] bool destroyOnCollide = false;
    [SerializeField] public float manaCost;
    protected Rigidbody2D rb;

    public static float time;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destTime);
    }

    
    public virtual void Shot(Vector2 dir)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward,dir);
        transform.Rotate(Vector3.forward,Random.Range(-spread,spread)+90);
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Bullet")) return;
        HPObject hp = coll.GetComponent<HPObject>();
        if(hp)
        {
            hp.RemoveHP(damage);
        }
        if(destroyOnCollide)
        {
            Destroy(gameObject);
        }
    }
}