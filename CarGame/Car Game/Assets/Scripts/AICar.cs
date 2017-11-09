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

    }

    // Update is called once per frame
    void Update()
    {
        //Car AI
        UpdateTargetNode();
        float m_targetAngle = UpdateTurnAmount();

        //Drive Forward
        //  Will Definitely need local obstacle check.

        m_rb.AddForce(transform.up * m_Accel, ForceMode2D.Force);

        if (m_targetAngle != 0.0f)
        {
            m_rb.AddTorque(m_targetAngle * m_Turning, ForceMode2D.Force);

        }


        /*
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            m_rb.AddForce(transform.up * m_Accel, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            m_rb.AddForce(-transform.up * (m_Accel * m_ReverseAccelFactor), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            m_rb.AddTorque(m_Turning * GetTurningPower(), ForceMode2D.Force);

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            m_rb.AddTorque(-m_Turning * GetTurningPower(), ForceMode2D.Force);

        }
        */




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

  
}
