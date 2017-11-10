using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//AI Steps
//Real Dumb AI - follows strict list of connect nodes.
//Better AI - can determine optimal set of nodes between 2 points
//Best AI - stopsif a car is in the way
//Godlike AI - won't turn into the side of another car

public class AICar : MonoBehaviour
{

    [Header("Driving Variables")]
    public float m_Accel;
    public float m_ReverseAccelFactor;
    public float m_Turning;

    [Header("Pathfinding Variables")]
    public List<RoadNode> m_Loop;
    public float m_TargetNodeDistanceThreshold;

    private PolygonCollider2D m_collider;
    private Rigidbody2D m_rb;
    private int m_targetNodeIndex;
    private float m_targetAngle;

    void Awake()
    {
        m_collider = GetComponent<PolygonCollider2D>();
        m_rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        m_rb.centerOfMass = new Vector2(0.0f, -0.4f);
        GenerateFullPath();

    }

    // Update is called once per frame
    void Update()
    {
        //Car AI
        UpdateTargetNode();
        float m_targetAngle = UpdateTurnAmount();

        //Drive Forward
        //  Will Definitely need local obstacle check. eventually.

        m_rb.AddForce(transform.up * m_Accel, ForceMode2D.Force);

        if (m_targetAngle != 0.0f)
        {
            m_rb.AddTorque(m_targetAngle * m_Turning, ForceMode2D.Force);

        }


    }

    public void GenerateFullPath()
    {
        List<RoadNode> newNodes = new List<RoadNode>();


        //assume loop (always for now)
        if (m_Loop[0] != m_Loop[m_Loop.Count - 1])
        {
            m_Loop.Add(m_Loop[0]);
        }

        for (int i = 0; i < m_Loop.Count; i++)
        {
            newNodes.Add(m_Loop[i]);
            if (i == m_Loop.Count - 1)
            {
                break;
            }
            newNodes.AddRange(FindPath(m_Loop[i], m_Loop[i + 1]));
            newNodes.RemoveAt(newNodes.Count - 1);//trim excess

        }

        m_Loop = newNodes;
    }

 

    private float UpdateTurnAmount()
    {
        //check angle to target node
        //  adjust turning radius
        //      will probably need a local obstacle check.
        
        float sign = Vector2.SignedAngle(
                         (transform.position + transform.up) - transform.position,
                         m_Loop[m_targetNodeIndex].transform.position - transform.position);
        if (Mathf.Abs(sign) <= 15.0f)
        {
            return 0.0f;
        }
        else
        {
            sign = sign > 0 ? 1.0f : -1.0f;
        }

    
        return sign * Mathf.Clamp01(Mathf.InverseLerp(0.2f, 1.0f, Mathf.Abs(m_rb.velocity.magnitude)));


    }

    private void UpdateTargetNode()
    {
        //Check distance to target node
        //  get new target node if needed
        

        float d = Vector2.Distance(transform.position, m_Loop[m_targetNodeIndex].transform.position);
        if (d <= m_TargetNodeDistanceThreshold)
        {
            m_targetNodeIndex++;
            if (m_targetNodeIndex >= m_Loop.Count)
            {
                m_targetNodeIndex = 0;
            }
        }

    }


    struct anode
    {
        public RoadNode parent;
        public float gCost;
        public float hCost;
        public float fCost;
    }


    List<RoadNode> FindPath(RoadNode startNode, RoadNode targetNode)
    {


        List<RoadNode> openSet = new List<RoadNode>();
        HashSet<RoadNode> closedSet = new HashSet<RoadNode>();
        Dictionary<RoadNode,anode > storeSet = new Dictionary<RoadNode,anode>();
        anode snode = new anode();
        snode.parent = startNode;
        snode.gCost = 0;
        snode.hCost = GetDistance(startNode, targetNode);
        snode.fCost = snode.hCost + snode.gCost;
        storeSet.Add(startNode, snode);
        openSet.Add(startNode);


        while (openSet.Count > 0)
        {
            RoadNode node = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (storeSet[openSet[i]].fCost < storeSet[node].fCost || storeSet[openSet[i]].fCost == storeSet[node].fCost)
                {
                    if (storeSet[openSet[i]].hCost < storeSet[node].hCost)
                        node = openSet[i];
                }
            }


            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                return RetracePath(startNode, targetNode, storeSet);

            }

            foreach (RoadNode neighbour in node.m_Neighbours)
            {

                if (closedSet.Contains(neighbour))
                {
                    continue;
                }
                float newCostToNeighbour = storeSet[node].gCost + GetDistance(node, neighbour);
                if (!openSet.Contains(neighbour))
                {

                    bool isStored = storeSet.ContainsKey(neighbour);
                    bool isLess = true;

                    if (isStored)
                    {
                        isLess = newCostToNeighbour < storeSet[neighbour].gCost;
                    }
                    if (isLess)
                    {

                        anode sode = new anode();

                        sode.gCost = newCostToNeighbour;
                        sode.hCost = GetDistance(neighbour, targetNode);
                        sode.parent = node;
                        sode.fCost = sode.hCost + sode.gCost;

                        if (isStored)
                        { 
                            storeSet[neighbour] = sode;
                        }
                        else
                        {
                            storeSet.Add(neighbour, sode);

                        }


                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }

    List<RoadNode> RetracePath(RoadNode startNode, RoadNode endNode, Dictionary<RoadNode, anode> stored)
    {
        List<RoadNode> path = new List<RoadNode>();
        RoadNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = stored[currentNode].parent;
        }
        path.Reverse();

        return path;

    }

    float GetDistance(RoadNode nodeA, RoadNode nodeB)
    {
        return Vector2.Distance(nodeA.transform.position, nodeB.transform.position);


    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);

        if (m_Loop != null)
        {
            if (m_Loop.Count > 0)
            {

                for (int i = 1; i < m_Loop.Count; i++)
                {
                    Gizmos.DrawLine(m_Loop[i - 1].transform.position, m_Loop[i].transform.position);

                }
                Gizmos.DrawLine(m_Loop[0].transform.position, m_Loop[m_Loop.Count - 1].transform.position);

            }
        }
    }
  
}
