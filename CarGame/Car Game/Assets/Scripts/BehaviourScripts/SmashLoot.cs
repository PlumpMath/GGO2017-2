using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashLoot : MonoBehaviour
{

    public bool m_Refills;
    public float m_RefillTimer;
    public LootTable m_Loot;

    private float m_refillCounter = 999;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_refillCounter >= m_RefillTimer)
        {
            m_refillCounter = 0;
            foreach (CollectibleObject.CollectibleType t in  m_Loot.GetCollectibleDrops())
            {
                StaticWorld.instance.Collectibles.SpawnEffect(transform.position, collision.relativeVelocity + (collision.relativeVelocity.magnitude * new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))), t);
            }
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
