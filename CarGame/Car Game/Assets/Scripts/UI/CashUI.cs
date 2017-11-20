using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashUI : CurrencyUI
{

    public WaveText m_Wave;

    public override void UpdateValue(int val)
    {
        m_Wave.speed = (Mathf.Lerp(0.1f, 5.0f, val / 1000.0f));
        m_Wave.magnitude = (Mathf.Lerp(0.1f, 2.0f, val / 500.0f));
        m_Value.text = val.ToString();
        base.UpdateValue(val);
    }
}
