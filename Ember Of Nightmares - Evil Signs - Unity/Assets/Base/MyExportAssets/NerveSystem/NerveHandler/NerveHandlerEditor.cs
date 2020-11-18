using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NerveHandlerMain))]
public class NerveHandlerEditor : Editor
{
    NerveHandlerMain nerveHandlerMain;
    SerializedProperty knownNerves;
    SerializedProperty knownNerveMains;

    private void OnEnable()
    {
        nerveHandlerMain = (NerveHandlerMain)target;
        knownNerves = serializedObject.FindProperty("knownNerves");
        knownNerveMains = serializedObject.FindProperty("knownNerveMains");
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Find my Nerves"))
        {
            nerveHandlerMain.GetAllMyNerves();
        }

        for (int i = 0; i < nerveHandlerMain.knownNerves.Count; i++)
        {
            EditorGUILayout.ObjectField(nerveHandlerMain.knownNerves[i], typeof(GameObject), false);
        }
        EditorGUILayout.Space(20);
        //for (int i = 0; i < nerveHandlerMain.knownNerveMains.Count; i++)
        //{
        //    EditorGUILayout.ObjectField(nerveHandlerMain.knownNerveMains[i], typeof(NerveMain), false);
        //}
    }
}
