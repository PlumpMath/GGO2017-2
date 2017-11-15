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

    public SpriteRenderer m_Visual;

    private CollectibleType m_Type;
    private CollectiblePooler m_Parent;


    public void Init(CollectiblePooler parent)
    {
        
    }

    public void Setup(CollectibleType type)
    {
        m_Visual.sprite = mSprites[(int)type];
        m_Type = type;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //hide / dectivate self.
        //transaction withplayer
        m_Parent.FlagCollectible(this);


    }
}
