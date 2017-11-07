using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    public float m_Accel;
    public float m_ReverseAccelFactor;
    public float m_Turning;

    private PolygonCollider2D m_collider;
    private Rigidbody2D m_rb;

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_rb.AddForce(transform.up * m_Accel, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_rb.AddForce(-transform.up * (m_Accel * m_ReverseAccelFactor), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_rb.AddTorque(m_Turning * GetTurningPower(), ForceMode2D.Force);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            m_rb.AddTorque(-m_Turning * GetTurningPower(), ForceMode2D.Force);

        }




    }

    private float GetTurningPower()
    {
        return Mathf.Clamp01(Mathf.InverseLerp(0.2f, 1.0f, Mathf.Abs(m_rb.velocity.magnitude)));
    }
}
