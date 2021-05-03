using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Armor))]
public class GenerateRandomArmorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (Armor)target;

        if (GUILayout.Button("Generate Random Stats"))
        {
            script.GenerateRandomGear();
        }

    }
}