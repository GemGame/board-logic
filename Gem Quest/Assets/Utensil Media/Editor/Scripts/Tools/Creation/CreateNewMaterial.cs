//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;

namespace EditorTools
{
    public class CreateNewMaterial 
    {
        [MenuItem("Project Tools/Tools/Create/Create Material", false, MenuPriorities.material)]
        static void MakeMaterial()
        {
            //Get selected objects
            Object[] selected = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
            //Loop through objecs
            for (int x = 0; x < selected.Length; x++)
            {
                string name = selected[x].name;
                //If the name contains "prefab" then remove that substring
                name = name.Replace("mat", "");
                string path = "Assets/Materials/mat" + name + ".mat";
                Object fileCheck = (Object)AssetDatabase.LoadAssetAtPath(path, typeof(Object));
                //If nothing is selected display dialogue else continue
                if (selected.Length == 0)
                {
                    EditorUtility.DisplayDialog("Error", "No selected texture to make material from.", "Ok");
                }
                else
                {
                    //If the path already exists
                    if (fileCheck)
                    {
                        //Prompt overwrite               
                        if (EditorUtility.DisplayDialog("Caution", "This material already exists, overwrite?", "Yes", "No"))
                        {
                            string texPath = AssetDatabase.GetAssetPath(selected[x]);
                            GenerateMaterial(path, texPath);
                        }
                    }
                    //Else just make the file
                    else
                    {
                        string texPath = AssetDatabase.GetAssetPath(selected[x]);

                        GenerateMaterial(path, texPath);
                    }
                }
            }
        }
        [MenuItem("Assets/Create Material")]
        static void RighClickMakeMaterial()
        {
            MakeMaterial();
        }
        static void GenerateMaterial(string localPath, string texPath)
        {
            //Create Material
            AssetDatabase.CreateAsset(new Material(Shader.Find("Diffuse")), localPath);
            //Reference Created Material
            Material material = (Material)(AssetDatabase.LoadAssetAtPath(localPath, typeof(Material)));
            //Assign Texture to Material
            material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(texPath, typeof(Texture2D));
            //Update Assets
            AssetDatabase.Refresh();
        }
    }
}
