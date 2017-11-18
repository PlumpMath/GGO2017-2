using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Loot Table")]
public class LootTable : ScriptableObject
{

    public List<CollectibleObject.CollectibleType> m_CollectibleDrops;
    public List<float> m_CollectibleWeights;
    public float AmountPerPull;

    public List<CollectibleObject.CollectibleType> GetCollectibleDrops()
    {
        List<CollectibleObject.CollectibleType> ret = new List<CollectibleObject.CollectibleType>();
        for (int i = 0; i < AmountPerPull; i++)
        {
            ret.Add(GetCollectible());
        }

        return ret;
    }

    private CollectibleObject.CollectibleType GetCollectible()
    {

        float total = 0;
        foreach (float f in m_CollectibleWeights)
        {
            total += f;
        }

        float mRand = Random.value * total;
        float check = 0;

        for (int i = 0; i < m_CollectibleWeights.Count; i++)
        {
            check += m_CollectibleWeights[i];
            if (check > mRand)
            {
                return m_CollectibleDrops[i];
            }

        }

        return m_CollectibleDrops[0];


    }
}
