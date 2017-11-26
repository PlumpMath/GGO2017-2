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
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            m_rb.AddForce(transform.up * m_Accel * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            m_rb.AddForce(-transform.up * (m_Accel * m_ReverseAccelFactor) * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            m_rb.AddTorque(m_Turning * GetTurningPower(), ForceMode2D.Force);

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            m_rb.AddTorque(-m_Turning * GetTurningPower(), ForceMode2D.Force);

        }

    }

    private float GetTurningPower()
    {
        return Mathf.Clamp01(Mathf.InverseLerp(0.2f, 1.0f, Mathf.Abs(m_rb.velocity.magnitude)));
    }

    public void GetHitByCop()
    {

        int cadCount = StaticWorld.instance.PlayerData.GetCurrencyForType(CollectibleObject.CollectibleType.Cadaver);
        int spawnCad = cadCount / 2;

        int ScrapCount = StaticWorld.instance.PlayerData.GetCurrencyForType(CollectibleObject.CollectibleType.Scrap);
        int spawnScrap = ScrapCount / 2;

        int GoldCount = StaticWorld.instance.PlayerData.GetCurrencyForType(CollectibleObject.CollectibleType.Gold);
        int spawnGold = GoldCount / 2;

        for (int i = 0; i < spawnCad; i++)
        {
            StaticWorld.instance.Collectibles.SpawnEffect(transform.position,
                new Vector2(Random.Range(-1.0f,1.0f), Random.Range(-1.0f,1.0f)),
                CollectibleObject.CollectibleType.Cadaver);

        }

        for (int i = 0; i < spawnScrap; i++)
        {
            StaticWorld.instance.Collectibles.SpawnEffect(transform.position,
                new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)),
                CollectibleObject.CollectibleType.Scrap);

        }

        for (int i = 0; i < spawnGold; i++)
        {
            StaticWorld.instance.Collectibles.SpawnEffect(transform.position,
                new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)),
                CollectibleObject.CollectibleType.Gold);

        }

        StaticWorld.instance.PlayerData.CurrencyTransaction(-cadCount, CollectibleObject.CollectibleType.Cadaver);
        StaticWorld.instance.PlayerData.CurrencyTransaction(-ScrapCount, CollectibleObject.CollectibleType.Scrap);
        StaticWorld.instance.PlayerData.CurrencyTransaction(-GoldCount, CollectibleObject.CollectibleType.Gold);

    }
}
