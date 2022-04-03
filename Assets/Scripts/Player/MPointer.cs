using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPointer : MonoBehaviour
{
    [SerializeField] float defMP;
    [SerializeField] Slider mpSlider;
    [SerializeField] float MPregen;
    float defMPR;

    public float MP;
    private void Start()
    {
        defMPR = MPregen;
        MP = defMP;
        if (mpSlider)
        {
            mpSlider.maxValue = MP;
            mpSlider.value = MP;
        }
    }
    public void ManaRegen()
    {
        ManaUsage(-MPregen*Time.deltaTime);
    }

    public bool ManaUsage(float manaCost)
    {
        if (manaCost > MP) return false;

        MP -= manaCost;
        if (MP > defMP)
            MP = defMP;
        if (mpSlider)
            mpSlider.value = MP;
        return true;
    }
    public void AddMpRegen(float mpr)
    {
        MPregen *= mpr;
    }
    public void RestoreMpRegen()
    {
        MPregen = defMPR;
    }
}
