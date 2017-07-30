using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(boardSquare))]
public class boardSquareGemEditor : Editor
{

    public override void OnInspectorGUI()
    {        
        boardSquare bs = (boardSquare)target;

        EditorGUILayout.LabelField("Gem Properties");
        bs.isStaticSquare = EditorGUILayout.Toggle("Static Square", bs.isStaticSquare);
        EditorGUILayout.IntField("X", bs.gemX);
        EditorGUILayout.IntField("Y", bs.gemY);
        EditorGUILayout.ObjectField("Gem", bs.gemPrefab, typeof(GameObject), false);
        
        if(GUILayout.Button("Change Gem"))
        {
            gemSelectorWindow window = new gemSelectorWindow();
            window.ShowWindow(bs);
        }
    }
}

public class gemSelectorWindow : EditorWindow
{
    int selection = 0;

    //EditorWindow window;
    boardSquare square;
    public void ShowWindow(boardSquare bs)
    {
        square = bs;
        var window = GetWindowWithRect<gemSelectorWindow>(new Rect(0, 0, 165, 100));
        window.Show();
    }
    void CloseWindow()
    {
        this.Close();
    }

    void ChangeSquareGem()
    {
        GameObject newGem = GetGemPoolObjects()[selection];
        baseGem newGemScript = newGem.GetComponent<baseGem>();

        square.GemScript.DestroyGem();
        newGemScript.SetGemProperties(square.transform.position, newGem);
        square.Gem = newGemScript.SpawnGemCopy(square.transform, newGemScript.GemGO, newGemScript.GemGO);
        square.gemPrefab = newGemScript.GemGO;
    }
    private void OnGUI()
    {
        selection = EditorGUILayout.Popup(selection, GetArrayObjectNames(GetGemPoolObjects()));
        foreach (GameObject gem in GetGemPoolObjects())
        {
            EditorGUILayout.ObjectField(gem, typeof(GameObject), false);
        }

        if(GUILayout.Button("Replace Gem"))
        {
            Debug.Log(GetGemPoolObjects()[selection]);
            ChangeSquareGem();
        }
        if (GUILayout.Button("Cancel"))
        {
            CloseWindow();
        }
    }
    GameObject GetObjectByName(string name)
    {
        foreach(GameObject go in GetGemPoolObjects())
        {
            if (go.name == name)
                return go;
        }
        return null;
    }
    GameObject[] GetGemPoolObjects()
    {
        gemPool gemPool = GameObject.FindGameObjectWithTag("Gem Pool").GetComponent<gemPool>();
        int arraySize = gemPool.Gems.Count;
        GameObject[] gems = new GameObject[arraySize];
        for(int x = 0; x < arraySize; x++)
        {
            gems[x] = gemPool.Gems[x];
        }

        return gems;
    }
    string[] GetArrayObjectNames(GameObject[] gems)
    {
        int arraySize = gems.Length;
        string[] names = new string[arraySize];

        for(int x = 0; x < arraySize; x++)
        {
            names[x] = gems[x].name;
        }
        return names;
    }
    
}
