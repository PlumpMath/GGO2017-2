using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OmniRoadManager))]
public class RoadManagerInspector : Editor
{

	
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("AutoNode All Children"))
        {
            ((OmniRoadManager)target).AutoNodeChildren();
        }

    }
}
