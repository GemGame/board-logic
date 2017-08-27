//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;
using System.IO;

namespace EditorTools
{
    [InitializeOnLoad]
    public class scriptFolderCreator 
    {
        const bool init = false;    //Default value for editor pref

        public static  bool Init
        {
            get { return EditorPrefs.GetBool("Init", init); }
            set { EditorPrefs.SetBool("Init", value); }
        }

       static scriptFolderCreator()
        {
            if (Init == false)  //If the folders have never been set up
            {
                MakeFolders();
                CacheClear.ClearCache();
            }
            Init = true;    //Then prevent that process from running again
        }
        //Generate Folders Within Our Project

        [MenuItem("Assets/Create/Project Folders", false, MenuPriorities.rightClickFolders)]
        static void ProjectCreateFolders()
        {
            MakeFolders();
        }
        //Add Menu Item
        [MenuItem("Project Tools/Project/Create Folders", false, MenuPriorities.folders)]
        static void MakeFolders()
        {
            //Store the path for folders
            string path = Application.dataPath + @"\";

            //Create Folders
            Directory.CreateDirectory(path + "Audio");
            Directory.CreateDirectory(path + "Materials");
            Directory.CreateDirectory(path + "Prefabs");
            Directory.CreateDirectory(path + "Scripts");
            Directory.CreateDirectory(path + "Meshes");
            Directory.CreateDirectory(path + "Textures");
            Directory.CreateDirectory(path + "Resources");
            Directory.CreateDirectory(path + "Shaders");
            Directory.CreateDirectory(path + "Packages");
            Directory.CreateDirectory(path + "Physics");
            Directory.CreateDirectory(path + "Scenes");
            Directory.CreateDirectory(path + "Scenes/Levels");
            Directory.CreateDirectory(path + "Scenes/Autosaves");

            //Refresh Assets
            AssetDatabase.Refresh();
        }
    }
}
