using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Accessory))]
public class GenerateRandomAccessoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (Accessory)target;

        if (GUILayout.Button("Generate Random Stats for Gear using Objects base stats (Level, rarity, etc.)"))
        {
            script.GenerateRandomGear();
        }

    }
}