using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniRoadManager : MonoBehaviour
{

    //this is data and shouldn't be set int he scene, really, but jams so
    public  ContactFilter2D m_WorldObstacles;

    public Color m_EditorColor;

    public void AutoNodeChildren()
    {
        foreach (RoadNode r in transform.GetComponentsInChildren<RoadNode>())
        {
            r.AutoNodeDetect();
        }
    }



}
