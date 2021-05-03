using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Weapon))]
public class GenerateRandomGearEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (Weapon)target;

        if (GUILayout.Button("Generate Random Stats for Gear using Objects base stats (Level, rarity, etc.)"))
        {
            script.GenerateRandomGear();
        }

    }
}