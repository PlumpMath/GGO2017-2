using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellingBooth : MonoBehaviour
{

    public CollectibleObject.CollectibleType m_Type;

    [Header("Economy")]
    public int BuyAmount;
    public int UpFrontCost;



    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            int amt = StaticWorld.instance.PlayerData.GetCurrencyForType(m_Type);
            if (amt > 0)
            {
                Time.timeScale = 0.0f;

                StaticWorld.instance.Seller.ShowSellingUI(m_Type, BuyAmount, UpFrontCost);

                //cost per = BuyAmount
                //Upfront = UpFront Cost;

                //todo this

            }
        }

    }
}
