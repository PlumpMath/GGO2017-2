using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{

    public Image m_Vis;
    public Text m_Value;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public virtual void UpdateValue(int val)
    {
        if (val == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            m_Value.text = val.ToString();
        }
    }
}
