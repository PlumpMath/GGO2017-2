using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePooler : MonoBehaviour
{

    public CollectibleObject CollectibleTemplate;
    [HideInInspector]
    public List<CollectibleObject> ActiveCollectibles = new List<CollectibleObject>();
    [HideInInspector]
    public List<CollectibleObject> InactiveCollectibles = new List<CollectibleObject>();
    [HideInInspector]
    public List<CollectibleObject> DirtyCollectibles = new List<CollectibleObject>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            CreateEffect();
        }

        CollectibleTemplate.gameObject.SetActive(false);
    }

    private CollectibleObject CreateEffect()
    {
        CollectibleObject tb = Instantiate<CollectibleObject>(CollectibleTemplate);
        tb.transform.SetParent(this.transform);
        tb.Init(this);
        DeactivateCollectible(tb);
        return tb;
    }

    public void SpawnEffect(Vector3 position, Sprite[] spriteList)
    {
        CollectibleObject ps = GetNextEffect();
        if (ps != null)
        {
            ps.gameObject.SetActive(true);
            ps.transform.position = position;
            ps.transform.Rotate(0, 0, Random.Range(0, 360));

            ps.Setup(CollectibleObject.CollectibleType.Gold);

            if (InactiveCollectibles.Contains(ps))
            {
                InactiveCollectibles.Remove(ps);
            }

            if (ActiveCollectibles.Contains(ps))
            {
                Debug.Log("trying to activate already active fx");
            }
            else
            {
                ActiveCollectibles.Add(ps);
            }
        }

    }


    private void DeactivateCollectible(Gib tb)
    {

        if (ActiveCollectibles.Contains(tb))
        {
            ActiveCollectibles.Remove(tb);
        }
        if (InactiveCollectibles.Contains(tb))
        {
            Debug.Log("trying to deactivate already deactivated Gib!");
        }
        else
        {
            InactiveCollectibles.Add(tb);
        }
    }

    private CollectibleObject GetNextEffect()
    {
        if (InactiveCollectibles.Count > 0)
        {
            CollectibleObject tb = InactiveCollectibles[0];
            InactiveCollectibles.RemoveAt(0);
            return tb;

        }
        else
        {
            //return null;
            return CreateEffect();
        }
    }

    public void FlagCollectible(CollectibleObject g)
    {
        DirtyCollectibles.Add(g);
    }

    // Update is called once per frame
    void Update()
    {


        foreach (CollectibleObject ps in DirtyCollectibles)
        {
            DeactivateCollectible(ps);
        }

        DirtyCollectibles.Clear();
    }
}
