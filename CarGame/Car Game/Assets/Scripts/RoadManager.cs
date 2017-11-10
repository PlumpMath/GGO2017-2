using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{

    //this is data and shouldn't be set int he scene, really, but jams so
    public  ContactFilter2D m_WorldObstacles;

    public void AutoNodeChildren()
    {
        foreach (RoadNode r in transform.GetComponentsInChildren<RoadNode>())
        {
            r.AutoNodeDetect();
        }
    }



}
