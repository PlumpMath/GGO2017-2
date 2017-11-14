using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibPooler : MonoBehaviour
{

    public Gib GibTemplate;
    [HideInInspector]
    public List<Gib> ActiveGibs = new List<Gib>();
    [HideInInspector]
    public List<Gib> InactiveGibs = new List<Gib>();
    [HideInInspector]
    public List<Gib> DirtyGibs = new List<Gib>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            CreateEffect();
        }

        GibTemplate.gameObject.SetActive(false);
    }

    private Gib CreateEffect()
    {
        Gib tb = Instantiate<Gib>(GibTemplate);
        tb.transform.SetParent(this.transform);
        tb.Init(this);
        DeactivateGib(tb);
        return tb;
    }

    public void SpawnEffect(Vector3 position, Sprite[] spriteList)
    {
        Gib ps = GetNextEffect();
        if (ps != null)
        {
            ps.gameObject.SetActive(true);
            ps.transform.position = position;
            ps.transform.Rotate(0, 0, Random.Range(0, 360));

            ps.SpawnGib(spriteList[Random.Range(0, spriteList.Length)], (new Vector2((Random.value * 2.0f) - 1.0f, (Random.value * 2.0f) - 1.0f).normalized));

            if (InactiveGibs.Contains(ps))
            {
                InactiveGibs.Remove(ps);
            }

            if (ActiveGibs.Contains(ps))
            {
                Debug.Log("trying to activate already active fx");
            }
            else
            {
                ActiveGibs.Add(ps);
            }
        }

    }


    private void DeactivateGib(Gib tb)
    {
        
        if (ActiveGibs.Contains(tb))
        {
            ActiveGibs.Remove(tb);
        }
        if (InactiveGibs.Contains(tb))
        {
            Debug.Log("trying to deactivate already deactivated Gib!");
        }
        else
        {
            InactiveGibs.Add(tb);
        }
    }

    private Gib GetNextEffect()
    {
        if (InactiveGibs.Count > 0)
        {
            Gib tb = InactiveGibs[0];
            InactiveGibs.RemoveAt(0);
            return tb;

        }
        else
        {
            return null;
            //return CreateEffect();
        }
    }

    public void FlagGib(Gib g)
    {
        DirtyGibs.Add(g);
    }
	
    // Update is called once per frame
    void Update()
    {
        

        foreach (Gib ps in DirtyGibs)
        {
            DeactivateGib(ps);
        }

        DirtyGibs.Clear();
    }
}
