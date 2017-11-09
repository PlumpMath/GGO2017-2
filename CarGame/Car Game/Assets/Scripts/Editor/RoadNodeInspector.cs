using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadNode))]
public class RoadNodeInspector : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUI.changed)
        {
            ((RoadNode)target).ValidateData();
        }
    }
}
