//Written By Christopher Cooke
//Gem Quest Board Square Inspector & Editor Window
//Custom inspector gives access to editor window
//Editor window allows convenient swapping of gems on game board during edit mode 
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

        if (GUILayout.Button("Change Gem"))
        {
            gemSelectorWindow window = new gemSelectorWindow();
            window.ShowWindow(bs);
        }
    }
}

public class gemSelectorWindow : EditorWindow
{
    int selection = 0;
    boardSquare square;

    //Window Control
    public void ShowWindow(boardSquare bs)
    {
        square = bs;
        var window = GetWindowWithRect<gemSelectorWindow>(new Rect(0, 0, 165, 100));
        InitializeDefaultSelection();
        window.Show();
    }
    void CloseWindow()
    {
        this.Close();
    }
    private void OnGUI()
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Gem To Replace With");
        EditorGUILayout.Separator();
        selection = EditorGUILayout.Popup(selection, GetArrayObjectNames(GetGemPoolObjects()));
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        if (GUILayout.Button("Replace Gem"))
        {
            Debug.Log(GetGemPoolObjects()[selection]);
            ChangeSquareGem();
        }
        EditorGUILayout.Separator();

        if (GUILayout.Button("Close"))
        {
            CloseWindow();
        }
    }

    //Button Action
    void ChangeSquareGem()
    {
        GameObject newGem = GetGemPoolObjects()[selection];
        baseGem newGemScript = newGem.GetComponent<baseGem>();

        square.GemScript.DestroyGem(false);
        newGemScript.SetGemProperties(square.transform.position, newGem);
        square.Gem = newGemScript.SpawnGemCopy(square.transform, newGemScript.GemGO, newGemScript.GemGO);
        square.gemPrefab = newGemScript.GemGO;
    }

    //Set Up Drop Down Menu
    void InitializeDefaultSelection()
    {
        GameObject[] gems = GetGemPoolObjects();
        for (int x = 0; x < gems.Length; x++)
        {
            if (gems[x] == square.gemPrefab)
            {
                selection = x;
                Debug.Log("Selection set to " + x);
            }
        }
    }
    GameObject GetObjectByName(string name)
    {
        foreach (GameObject go in GetGemPoolObjects())
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
        for (int x = 0; x < arraySize; x++)
        {
            gems[x] = gemPool.Gems[x];
        }

        return gems;
    }
    string[] GetArrayObjectNames(GameObject[] gems)
    {
        int arraySize = gems.Length;
        string[] names = new string[arraySize];

        for (int x = 0; x < arraySize; x++)
        {
            names[x] = gems[x].name;
        }
        return names;
    }
}