using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gib : MonoBehaviour
{


    private SpriteRenderer m_Sprite;
    private Rigidbody2D m_rb;
    private bool m_active;
    private GibPooler m_Pool;


    public void Init(GibPooler pool)
    {
        m_Pool = pool;
        m_Sprite = GetComponent<SpriteRenderer>();
        m_rb = GetComponent <Rigidbody2D>();
        m_active = false;
    }


    public void SpawnGib(Sprite sp, Vector2 Impulse)
    {
        m_Sprite.sprite = sp;
        m_Sprite.color = Color.white;
        m_rb.simulated = true;
        m_rb.AddForce(Impulse, ForceMode2D.Impulse);
        m_rb.AddTorque(Random.Range(-2.0f, 2.0f));
        m_active = true;

    }

    void OnBecameInvisible()
    {
        if (m_active)
        {
            m_active = false;
            m_Sprite.color = Color.clear;
            m_rb.simulated = false;
            m_Pool.FlagGib(this);
        }
    }



}
