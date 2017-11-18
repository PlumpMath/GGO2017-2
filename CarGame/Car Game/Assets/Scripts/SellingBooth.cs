using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBooth : MonoBehaviour
{

    public CollectibleObject.CollectibleType m_Type;

    [Header("Economy")]
    public int BuyAmount;
    public int UpFrontCost;


    private int saleCount;

    void OnTriggerEnter2D(Collider2D collider)
    {
        int amt = StaticWorld.instance.PlayerData.GetCurrencyForType(m_Type);
        if (amt > 0)
        {
            
            StaticWorld.instance.PlayerData.CashTransaction((amt * BuyAmount) - UpFrontCost);
            StaticWorld.instance.PlayerData.CurrencyTransaction(-amt, m_Type);

        }


    }
}
