using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RoadNode : MonoBehaviour
{

    public List<RoadNode> m_Neighbours;

    private OmniRoadManager m_RoadManager;

    void OnDrawGizmos()
    {
        if (m_RoadManager == null)
        {
            m_RoadManager = GameObject.FindObjectOfType<OmniRoadManager>();
        }
        Gizmos.color = m_RoadManager.m_EditorColor;
        Gizmos.DrawSphere(transform.position, 0.25f);
        Handles.Label(transform.position + (Vector3.up * 0.5f), gameObject.name);

        if (m_Neighbours != null)
        {
            foreach (RoadNode a in m_Neighbours)
            {
                if (a != null)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(transform.position, a.transform.position);
                    Gizmos.DrawCube(Vector3.Lerp(transform.position, a.transform.position, 0.86f), Vector3.one * 0.1f);
                    Gizmos.DrawCube(Vector3.Lerp(transform.position, a.transform.position, 0.85f), Vector3.one * 0.2f);
                    Gizmos.DrawCube(Vector3.Lerp(transform.position, a.transform.position, 0.83f), Vector3.one * 0.3f);


                }
            }
        }
    }

    public void AutoNodeDetect()
    {
        m_Neighbours.Clear();

        if (m_RoadManager == null)
        {
            m_RoadManager = GameObject.FindObjectOfType<OmniRoadManager>();
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
        /*
        //we ensure the relationship is 2 ways
        //is this good idea im not sure

        //NARRATOR: It wasn't

        foreach (RoadNode a in m_Neighbours)
        {
            if (!a.m_Neighbours.Contains(this))
            {
                a.m_Neighbours.Add(this);
            }
        }*/
    }
}
