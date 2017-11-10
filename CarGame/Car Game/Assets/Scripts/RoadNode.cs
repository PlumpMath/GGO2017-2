using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadNode : MonoBehaviour
{

    public List<RoadNode> m_Neighbours;

    private RoadManager m_RoadManager;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);

        if (m_Neighbours != null)
        {
            foreach (RoadNode a in m_Neighbours)
            {
                if (a != null)
                {
                    Gizmos.DrawLine(transform.position, a.transform.position);
                }
            }
        }
    }

    public void AutoNodeDetect()
    {
        m_Neighbours.Clear();

        if (m_RoadManager == null)
        {
            m_RoadManager = GameObject.FindObjectOfType<RoadManager>();
        }

        RoadNode[] nodes = GameObject.FindObjectsOfType<RoadNode>();
        for (int i = 0; i < nodes.Length; i++)
        {

            float distance = Vector2.Distance(nodes[i].transform.position, transform.position);
            if (
                distance <= 20.0f &&
                nodes[i] != this)
            {

                RaycastHit2D[] mcast = new RaycastHit2D[1];
                Physics2D.CircleCast(transform.position,
                    0.5f,
                    nodes[i].transform.position - transform.position,
                    m_RoadManager.m_WorldObstacles,
                    mcast,
                    distance);

                if (mcast[0].collider == null)
                {
                    m_Neighbours.Add(nodes[i]);

                }
            }
        }
    }

    public void ValidateData()
    {

        //we ensure the relationship is 2 ways
        //is this good idea im not sure
        foreach (RoadNode a in m_Neighbours)
        {
            if (!a.m_Neighbours.Contains(this))
            {
                a.m_Neighbours.Add(this);
            }
        }
    }
}
