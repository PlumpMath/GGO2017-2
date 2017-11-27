using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public CurrencyUI m_Cadaver;
    public CurrencyUI m_Gold;
    public CurrencyUI m_Scrap;
    public CurrencyUI m_Cash;

    public GameOverUI m_GameOver;

    public void SetCash(int val)
    {
        m_Cash.UpdateValue(val);
    }

    public void SetCurrency(CollectibleObject.CollectibleType type, int val)
    {
        switch (type)
        {
            case CollectibleObject.CollectibleType.Cadaver:
                m_Cadaver.UpdateValue(val);
                break;
            case  CollectibleObject.CollectibleType.Gold:
                m_Gold.UpdateValue(val);
                break;
            case CollectibleObject.CollectibleType.Scrap:
                m_Scrap.UpdateValue(val);
                break;
        }
    }
}
