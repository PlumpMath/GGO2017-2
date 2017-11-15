using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashLoot : MonoBehaviour
{

    public bool m_Refills;
    public float m_RefillTimer;

    private float m_refillCounter = 999;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_refillCounter >= m_RefillTimer)
        {
            Debug.Log("Smashed");
            m_refillCounter = 0;
        }
    }

    void Update()
    {
        if (m_Refills)
        {
            if (m_refillCounter < m_RefillTimer)
            {
                m_refillCounter += Time.deltaTime;
            }
        }
    }
}
