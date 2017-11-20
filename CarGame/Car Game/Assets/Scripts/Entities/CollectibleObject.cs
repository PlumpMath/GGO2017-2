using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{

    public enum CollectibleType
    {
        Gold = 0,
        Scrap = 1,
        Cadaver = 2}

    ;

    public Sprite[] mSprites;
    public LayerMask m_PlayerLayer;

    public SpriteRenderer m_Visual;
    public Rigidbody2D m_rb;



    private CollectibleType m_Type;
    private CollectiblePooler m_Parent;

    private bool m_canBeCollected;


    public void Init(CollectiblePooler parent)
    {
        m_Parent = parent;
        m_Visual.color = Color.clear;
        m_rb.simulated = false;

    }

    public void SpawnCollectible(CollectibleType type, Vector3 dir)
    {
        m_Visual.sprite = mSprites[(int)type];
        m_Type = type;
        m_Visual.color = Color.white;
        m_rb.simulated = true;
        m_canBeCollected = true;
        m_rb.AddForce(dir.normalized, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (m_rb.simulated)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                //hide / dectivate self.
                m_Visual.color = Color.clear;
                m_rb.simulated = false;
                //transaction withplayer
                StaticWorld.instance.PlayerData.CurrencyTransaction(1, m_Type);
                m_Parent.FlagCollectible(this);
            }
            else
            {
                m_rb.velocity = -m_rb.velocity;//so hacky
            }
        }


    }
}
