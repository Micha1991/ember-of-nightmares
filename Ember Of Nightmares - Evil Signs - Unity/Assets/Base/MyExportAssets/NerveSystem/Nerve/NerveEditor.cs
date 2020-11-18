using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
[CustomEditor(typeof(NerveMain))]
public class NerveEditor : Editor
{
    NerveMain nerveMain;
    private ChangeCheck<LayerMask> changeCheckLayerMask = new ChangeCheck<LayerMask>();
    SerializedProperty nerveList;
    List<bool> useFoldoutForTags = new List<bool>();
    SerializedProperty myTriggerCollider;
    SerializedProperty hitColliders;

    private void OnEnable()
    {
        nerveMain = (NerveMain)target;
        nerveList = serializedObject.FindProperty("nerveList");
        myTriggerCollider = serializedObject.FindProperty("myTriggerCollider");
        hitColliders = serializedObject.FindProperty("hitColliders");
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space(20);
        serializedObject.Update();
        EditorGUILayout.ObjectField(myTriggerCollider);
        EditorGUILayout.PropertyField(hitColliders);
        EditorGUILayout.Space(20);

        GUILayout.Label("Layers to detect", EditorStyles.boldLabel);
        LayerMask tempMask = EditorGUILayout.MaskField(InternalEditorUtility.LayerMaskToConcatenatedLayersMask(nerveMain.iWantDetect), InternalEditorUtility.layers);
        nerveMain.iWantDetect = InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
        if (changeCheckLayerMask.checkChange(nerveMain.iWantDetect))
        {
            nerveMain.RefreshAllLayers();
            RefreshFoldoutUse();
        }
        EditorGUILayout.Space(20);


        EditorGUILayout.LabelField("Choosen Layers and Tags", EditorStyles.boldLabel);
        for (int i = 0; i < nerveMain.nerveList.ToList().Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            useFoldoutForTags[i] = EditorGUILayout.BeginFoldoutHeaderGroup(useFoldoutForTags[i], nerveMain.nerveList[i].layerName);
            EditorGUILayout.Toggle(nerveMain.nerveList[i].layerActive);
            EditorGUILayout.EndHorizontal();
            if (nerveMain.nerveList[i].tagNames.Count == 0)
            {
                if (GUILayout.Button("Use Tags"))
                {
                    nerveMain.AddTag(i, 0);
                }
            }
            else
            {

                for (int j = 0; j < nerveMain.nerveList[i].tagNames.Count; j++)
                {
                    
                    if (useFoldoutForTags[i])
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space(20);
                        if (GUILayout.Button("+"))
                        {
                            nerveMain.AddTag(i, j + 1);
                        }
                        nerveMain.nerveList[i].tagNames[j] = EditorGUILayout.TextField(nerveMain.nerveList[i].tagNames[j]);
                        EditorGUILayout.Toggle(nerveMain.nerveList[i].tagActive[j]);
                        if (GUILayout.Button("-"))
                        {
                            nerveMain.DeleteTag(i, j);
                            return;
                        }
                        EditorGUILayout.EndHorizontal();
                        if (!nerveMain.CheckIfTagExists(nerveMain.nerveList[i].tagNames[j]))
                        {
                            var messageText = "Attention: This Tag does not exist in this project. Tag will be ignored!";
                            EditorGUILayout.HelpBox(messageText, MessageType.Warning);
                        }
                    }
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        //EditorGUILayout.PropertyField(nerveList);

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space(40);
        if (GUILayout.Button("Reset All"))
        {
            serializedObject.Update();
            nerveMain.RefreshProjectLayerChanges();
            serializedObject.ApplyModifiedProperties();
            return;
        }
    }

    void RefreshFoldoutUse()
    {
        useFoldoutForTags = new List<bool>();
        for (int i = 0; i < nerveMain.nerveList.Count; i++)
        {
            useFoldoutForTags.Add(true);
        }
    }
}
