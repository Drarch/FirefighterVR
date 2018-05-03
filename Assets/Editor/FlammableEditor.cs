using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Flammable))]
public class FlammableEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.Separator();

        Flammable item = (Flammable)target;

        if (GUILayout.Button(item.IsOnFire ? "Put out Fire" : "Set on Fire"))
        {
            if (item.IsOnFire)
            {
                item.Temerature = 20;
                item.IsOnFire = false;
                item.StopFire();
            }
            else
            {
                item.Temerature = item.fireTemperature;
                item.IsOnFire = true;
                item.StartFire();
            }
        }
    }
}
