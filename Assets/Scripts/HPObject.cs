using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
//скрипт хп
public class HPObject : NetworkBehaviour
{
    [SerializeField] float defHP;

    [SyncVar]
    [SerializeField] float HP;
    [SyncVar]
    [SerializeField] float HPregen;
    [SerializeField] Slider hpSlider;
    private void Start()
    {
        HP = defHP;
        if (hpSlider)
        {
            hpSlider.maxValue = HP;
            hpSlider.value = HP;
        }
    }

    public void RemoveHP(float damage)
    {
        HP -= damage;
        if (hpSlider)
            hpSlider.value = HP;
        if (HP > 100)
            HP = defHP;
        if (HP <= 0)
        {
            Delete();
            HP = defHP;
        }
    }
    public void HealthRegen()
    {
        RemoveHP(-HPregen * Time.deltaTime);
    }
    public void AddHpRegen(float hpr)
    {
        HPregen += hpr;
    }
    protected virtual void Delete()
    {
        Destroy(gameObject);
    }
}