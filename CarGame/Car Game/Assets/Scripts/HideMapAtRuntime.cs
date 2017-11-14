using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class HideMapAtRuntime : MonoBehaviour
{
    public bool m_active;

    // Use this for initialization
    void Awake()
    {
        if (m_active)
        {
            GetComponent<Tilemap>().color = Color.clear;
        }
    }
	
}
