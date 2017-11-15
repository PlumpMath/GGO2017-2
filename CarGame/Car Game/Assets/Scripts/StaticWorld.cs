using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class StaticWorld : MonoBehaviour
{

    public static StaticWorld instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }





    public GibPooler Gibs;

    public PlayerData PlayerData;
}
