using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Gear))]
public class GenerateRandomGearButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (Weapon)target;

        if (GUILayout.Button("Generate Random Stats for Gear using Objects base stats (Level, rarity, etc.)", GUILayout.Height(40), GUILayout.Width(100)))
        {
            script.GenerateRandomGear();
        }

    }
}