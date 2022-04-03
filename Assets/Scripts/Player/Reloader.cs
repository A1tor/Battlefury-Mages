using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reloader : MonoBehaviour
{
    [SerializeField] float baseReloadTime;
    float reloadTime;
    [SerializeField] float startAlfa;
    [SerializeField] float endAlfa;

    [SerializeField] Image[] images;
    List<bool> ready = new List<bool>();

    int maxCharge;
    public int curCharge = 0;

    void Start()
    {
        reloadTime = baseReloadTime * maxCharge;
        maxCharge = images.Length;
        foreach(Image img in images)
        {
            img.color = new Color(1, 1, 1, startAlfa);
            ready.Add(false);
        }
        StartCoroutine(Load());
    }

    bool HasCharge()
    {
        return ready.Contains(true);
    }
    public bool RemoveCharge()
    {
        if (!HasCharge()) return false;
        for(int i = maxCharge-1; i>=0; i--)
        {
            if(ready[i])
            {
                curCharge--;
                ready[i] = false;
                images[i].color = new Color(1, 1, 1, startAlfa);
                return true;
            }
        }
        return false;
    }

    IEnumerator Load()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.01f);
            for(short i = 0; i<maxCharge; i++)
            {
                if (ready[i]) continue;
                images[i].color = new Color(1, 1, 1, images[i].color.a + 0.01f/(baseReloadTime*maxCharge/(curCharge+1)));
                if (images[i].color.a >= endAlfa)
                {
                    ready[i] = true;
                    curCharge++;
                }
            }
        }
    }

    public virtual void OnLoaded() { }
}
