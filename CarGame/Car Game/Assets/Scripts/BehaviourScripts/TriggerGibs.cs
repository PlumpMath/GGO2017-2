using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGibs : MonoBehaviour
{

    public Sprite[] m_GibVis;
    public int m_Count;

    void OnTriggerEnter2D(Collider2D m_Collider)
    {
        for (int i = 0; i < m_Count; i++)
        {
            StaticWorld.instance.Gibs.SpawnEffect(transform.position, m_GibVis);
        }


        Destroy(this.gameObject);
    }
}
