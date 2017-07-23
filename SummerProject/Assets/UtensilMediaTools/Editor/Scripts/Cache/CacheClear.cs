//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

namespace EditorTools
{
    [ExecuteInEditMode]
    class CacheClear
    {
        [MenuItem("Project Tools/Editor Settings/Cache/Clear All Cache")]
        public static void ClearCache()    //Run all three clears
        {
            if (EditorUtility.DisplayDialog("WARNING", "This will permenately delete all editor script resources! \nThis includes clipboard objects, level templates, and autosaves.\n\nContinue?", "Yes", "No"))
            {
                ClearAutosave();
                ClearClipboard();
                ClearLevelTemplate();
            }
            AssetDatabase.Refresh();
        }
        [MenuItem("Project Tools/Editor Settings/Cache/Clear Autosaves")]
        static void ClearAutosave() //Recursively delete autosave folder
        {
            Directory.Delete(Application.dataPath + @"/Scenes/Autosaves", true);
            AssetDatabase.Refresh();
        }
        [MenuItem("Project Tools/Editor Settings/Cache/Clear Clipboard")]
        static void ClearClipboard()    //Recursively delete object clipboard resource folder
        {
            ObjectClipboard.ClearClipboard();
            AssetDatabase.Refresh();
        }
        [MenuItem("Project Tools/Editor Settings/Cache/Clear Level Template")]
        static void ClearLevelTemplate()    //Recursively delete level template folder
        {
            CreateNewLevel.ClearLevelTemplate();
            AssetDatabase.Refresh();
        }

    }
}
