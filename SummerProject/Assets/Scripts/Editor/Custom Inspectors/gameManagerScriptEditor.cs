using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(gameManager))]
public class gameManagerScriptEditor : Editor {

    public override void OnInspectorGUI()
    {
        string[] directions = { "Down", "Right", "Left", "Up" };
        int[] values = { 0, 1, 2, 3 };
        gameManager gm = (gameManager)target;

        EditorGUILayout.LabelField("Current Falling Direction");
        gm.BoardManager.CurrentDirection = EditorGUILayout.IntPopup(gm.BoardManager.CurrentDirection, directions, values);
        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);
        
    }
}


