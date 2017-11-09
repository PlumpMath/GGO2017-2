using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{

    public List<RoadNode> m_Neighbours;

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);

        if (m_Neighbours != null)
        {
            foreach (RoadNode a in m_Neighbours)
            {
                Gizmos.DrawLine(transform.position, a.transform.position);
            }
        }
    }

    public void ValidateData()
    {

        //we ensure the relationship is 2 ways
        foreach (RoadNode a in m_Neighbours)
        {
            if (!a.m_Neighbours.Contains(this))
            {
                a.m_Neighbours.Add(this);
            }
        }
    }
}
