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
            Undo.RecordObject(target, "Is On Fire Button");

            if (item.IsOnFire)
            {
                item.Temperature = 20;
                item.IsOnFire = false;
                item.StopFire();
            }
            else
            {
                item.FindObjectInHeatRadius();
                item.Temperature = item.fireTemperature;
                item.IsOnFire = true;
                item.StartFire();
            }
        }
    }
}
