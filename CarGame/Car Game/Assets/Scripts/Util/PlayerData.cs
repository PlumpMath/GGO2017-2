using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public int CadaverCount;
    public int GoldCount;
    public int ScrapCount;

    public int TotalCash;

    public float WantedLevel;


    public void CurrencyTransaction(int dif, CollectibleObject.CollectibleType type)
    {
        switch (type)
        {
            case CollectibleObject.CollectibleType.Cadaver:
                CadaverCount += dif;
                StaticWorld.instance.mUI.SetCurrency(type, CadaverCount);
                break;
            case CollectibleObject.CollectibleType.Gold:
                GoldCount += dif;
                StaticWorld.instance.mUI.SetCurrency(type, GoldCount);
                break;
            case CollectibleObject.CollectibleType.Scrap:
                ScrapCount += dif;
                StaticWorld.instance.mUI.SetCurrency(type, ScrapCount);
                break;
        }


    }

    public void CashTransaction(int dif)
    {
        TotalCash += dif;
        StaticWorld.instance.mUI.SetCash(TotalCash);
    }

}
