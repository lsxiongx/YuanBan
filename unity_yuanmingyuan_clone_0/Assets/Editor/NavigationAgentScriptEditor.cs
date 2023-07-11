using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(NavigationAgentScript))]
public class NavigationAgentScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        NavigationAgentScript ys = target as NavigationAgentScript;
        if (ys)
        {
            UnityEngine.AI.NavMeshAgent anvi = ys.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (anvi == null)
            {
                anvi = ys.gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
                Debug.Log("Load NavMeshAgent");
            }
        }

        if (GUILayout.Button("StartNavi"))
        {
            ys.OnStartAINavigation();
        }
        if (GUILayout.Button("StartCustomNavi"))
        {
            ys.OnStartCustomNavigation();
        }
        if (GUILayout.Button("StopNavi"))
        {
            ys.OnAutoNavigation();
        }

    }
}
