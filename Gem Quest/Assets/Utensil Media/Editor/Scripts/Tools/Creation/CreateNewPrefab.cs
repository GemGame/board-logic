//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;

namespace EditorTools
{
    public class CreateNewPrefab : MonoBehaviour
    {

        [MenuItem("Project Tools/Tools/Create/Create Prefab", false, MenuPriorities.prefabs)]
        static void MakePrefab()
        {
            //Get selected objects
            GameObject[] selected = Selection.gameObjects;
            //Loop through objecs
            for (int x = 0; x < selected.Length; x++)
            {
                string name = selected[x].gameObject.name;
                //If the name contains "prefab" then remove that substring
                name = name.Replace("prefab", "");
                string path = "Assets/Prefabs/prefab" + name + ".prefab";
                GameObject fileCheck = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));                
                //If the path already exists
                if (fileCheck)
                {
                    //Prompt overwrite               
                    if (EditorUtility.DisplayDialog("Caution", "This prefab already exists, overwrite?", "Yes", "No"))
                    {

                        GeneratePrefab(path, selected[x]);
                    }
                }
                //Else just make the file
                else
                {
                    GeneratePrefab(path, selected[x]);
                }

            }

        }
        [MenuItem("GameObject/Create Prefab", false, 0)]
        static void RightClickMakePrefab()
        {
            MakePrefab();
        }

        static GameObject GeneratePrefab(string localPath, GameObject selected)
        {
            //Create empty prefab
            Object prefab = PrefabUtility.CreateEmptyPrefab(localPath);
            //Assign object to prefab
            PrefabUtility.ReplacePrefab(selected, prefab);
            //Refresh Assets
            AssetDatabase.Refresh();
            //Destroy the original immediately
            DestroyImmediate(selected);
            //Instantiate new game object from prefab
            GameObject newPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            
            return newPrefab;  //Needed to use value to be bug free       
        }
    }
}
