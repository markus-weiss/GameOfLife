using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameOfLife))]
public class GoLEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        GameOfLife refTarget = (GameOfLife)target;

        if (GUILayout.Button("Create"))
            refTarget.Create();

        if (GUILayout.Button("Next Generation"))
            refTarget.NextGeneration();

        if (GUILayout.Button("Run"))
            refTarget.Run();

        if (GUILayout.Button("Stop"))
            refTarget.Stop();

        if (GUILayout.Button("Destroy"))
            refTarget.Destroy();
    }
}
