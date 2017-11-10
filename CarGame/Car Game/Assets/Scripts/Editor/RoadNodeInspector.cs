using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadNode))]
[CanEditMultipleObjects]
public class RoadNodeInspector : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("AutoNode"))
        {
            ((RoadNode)target).AutoNodeDetect();
        }

        if (GUI.changed)
        {
            ((RoadNode)target).ValidateData();
        }
    }
}
