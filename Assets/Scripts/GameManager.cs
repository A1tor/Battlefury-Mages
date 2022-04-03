using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameStates
    {
        Menu,
        Game,
        Shop
    }
    [SerializeField] GameStates gs;
    [SerializeField] GameObject panelMenu;
    [SerializeField] GameObject panelShop;
    [SerializeField] GameObject controllArea;
    [SerializeField] float MPotEff;
    [SerializeField] float HPotEff;

    MPointer mp;
    HPObject hp;
    Mage mage;

    float  MPoTime = 0, HPoTime = 0;
    void Start()
    {
        ScreenUpdate();
        mage = FindObjectOfType<Mage>();
        mp = mage.GetComponent<MPointer>();
        hp = mage.GetComponent<HPObject>();
    }
    void FixedUpdate()
    {
        if (HPoTime > 0)
            HPoTime -= Time.fixedDeltaTime;
        if (MPoTime > 0)
            MPoTime -= Time.fixedDeltaTime;
        else mp.RestoreMpRegen();
    }
    void ScreenUpdate()
    {
        panelMenu.SetActive(false);
        panelShop.SetActive(false);
        switch (gs)
        {
            case GameStates.Game:
                controllArea.SetActive(true);
                break;
            case GameStates.Menu:
                controllArea.SetActive(false);
                panelMenu.SetActive(true);
                break;
            case GameStates.Shop:
                panelShop.SetActive(true);
                break;
        }
    }

    public void ClickMenu()
    {
        if (gs != GameStates.Menu) gs = GameStates.Menu;
        else gs = GameStates.Game;
        ScreenUpdate();
    }
    public void ClickShop()
    {
        switch(gs)
        {
            case GameStates.Game:
                gs = GameStates.Shop;
                break;
            default:
                gs = GameStates.Game;
                break;
        }
        ScreenUpdate();
    }
    public void ClickExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void ClickDash()
    {
        mage.Dash();
    }

    public void ClickUlt()
    {
        mage.Ult();
    }
    public void BuyHPotion()
    {
        HPoTime =10;
        hp.AddHpRegen(HPotEff);
    }
    public void BuyMPotion()
    {
        MPoTime =10;
        mp.AddMpRegen(MPotEff);
    }
    public void BuyTrap()
    {

    }
    public void ClickRid()
    {

    }
    public void StartRevive(GameObject corpse, float time)
    {
        StartCoroutine(Revive(corpse, time));
    }

    IEnumerator Revive(GameObject corpse, float time)
    {
        corpse.SetActive(false);
        yield return new WaitForSeconds(time);
        corpse.SetActive(true);
    }
    
}