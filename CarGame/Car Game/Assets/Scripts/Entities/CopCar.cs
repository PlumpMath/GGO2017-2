using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopCar : AICar
{

    public enum CopState
    {
        patrolling,
        chasing,
        waiting
    }

    public bool isPissed;
    public float m_ChaseAccel;

    public CopState m_State = CopState.patrolling;
    public LayerMask m_PlayerLayer;
    public float m_StunDuration;


    private PlayerCar m_Player;

    private float m_counter = 0;

    private RaycastHit2D[] m_results = new RaycastHit2D[1];
    //need nodelist for the chase - will be custom.
    //will rpobably ave to refactor AICar to pass its nodelist into its helper functions ratherthan rely on a local list.
    //then build nodegraph to player if playerlocation is known and cop is pissed.

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        switch (m_State)
        {
            case CopState.patrolling:

                //default car actions
                base.Update();

                break;
            case CopState.waiting:

                m_counter += Time.deltaTime;
                if (m_counter > m_StunDuration)
                {
                    StartChasingPlayer();
                }
                //do nothing?
                break;
            case CopState.chasing:

                if (isPissed)
                {
                    isPissed = CanSeePlayer();

                    if (!isPissed)
                    {
                        OverrideTargetNode(GetNearestNodeFromGraph(m_Loop));
                    }


                    float m_targetAngle = UpdateTurnAmount(m_Player.transform);


                    //wasting some work here.  probably fine for jam though

                    float sign = Vector2.SignedAngle(
                                     (transform.position + transform.up) - transform.position,
                                     m_Player.transform.position - transform.position);

                    if (Mathf.Abs(sign) > 90.0f)
                    {
                        m_rb.AddForce((-transform.up) * m_ChaseAccel * Time.deltaTime, ForceMode2D.Force);

                    }
                    else
                    {
                        m_rb.AddForce(transform.up * m_ChaseAccel * Time.deltaTime, ForceMode2D.Force);

                    }

                    if (m_targetAngle != 0.0f)
                    {
                        
                        m_rb.AddTorque(m_targetAngle * m_Turning, ForceMode2D.Force);
                    }
                }
                else
                {
                    isPissed = CanSeePlayer();

                    
                    base.Update();
                }
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (m_State)
        {
            case CopState.chasing:

                if (collision.collider.CompareTag("Player"))
                {
                    //TODOcontinue
                }
                
                break;
            case CopState.patrolling:
                if (collision.collider.CompareTag("Player"))
                {
                    m_Player = collision.collider.GetComponent<PlayerCar>();
                    m_rb.AddForce(collision.relativeVelocity * 0.1f, ForceMode2D.Impulse);
                    StartWait();
                }

                break;
            case CopState.waiting:
                break;
        }
    }


    private bool CanSeePlayer()
    {
        Physics2D.CircleCastNonAlloc(
            transform.position, 0.25f,
            m_Player.transform.position - transform.position,
            m_results,
            20.0f,
            m_PlayerLayer
        );
        if (m_results[0].collider != null)
        {
            if (m_results[0].collider.gameObject == m_Player.gameObject)
            {
                return true;
            }
            return false;
        }

        return false;
    }

    private void StartChasingPlayer()
    {
        m_State = CopState.chasing;

        isPissed = CanSeePlayer();

    }

    private void StartWait()
    {
        m_State = CopState.waiting;
        m_counter = 0;
    }


}
