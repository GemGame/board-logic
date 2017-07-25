//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;
using System.IO;

namespace EditorTools
{
    [ExecuteInEditMode]
    public class ObjectClipboard : MonoBehaviour
    {
        //Private variables
        public static GameObject[] allObjects;        
        static string clipboardPath = "Assets/UtensilMediaTools/Editor/Resources/Clipboard/";
        public static bool cut = false;     
        
        public static void AfterExit() //Actions for after exiting playmode
        {           
            if (cut == true) //If we cut some object out
            {
                cut = false;               
                for (int x = 0; x < allObjects.Length; x++)     //Delete those objects
                {
                    Debug.Log("Destroying thing : " + x);
                    DestroyImmediate(allObjects[x]);
                }
            }
        }          
        public static void MakePrefab(GameObject go, string path) //Create a prefab of a game object
        {            
            //Make a prefab in the clipboard directory from a passed in game object
            PrefabUtility.CreatePrefab(path + go.name + ".prefab", go);
        }
        public static void MakePrefabs(GameObject[] gos, string path, int itemNum) //Create  prefabs from array of GameObject
        {            
            for (int x = 0; x < itemNum; x++)  //Loop through objects
            {
                RenameDuplicates(x);
                //Make a prefab in the clipboard directory from a passed in game object
                MakePrefab(gos[x].transform.root.gameObject, clipboardPath);
            }
        }
        public static void MakePrefabsSelected(GameObject[] gos, string path, int itemNum) //Create  prefabs from array of GameObject
        {

            for (int x = 0; x < itemNum; x++)  //Loop through objects
            {
                RenameDuplicates(x);
                //Make a prefab in the clipboard directory from a passed in game object
                MakePrefab(gos[x].transform.gameObject, clipboardPath);
            }
        }
        [MenuItem("GameObject/Clipboard/Copy/All", false, MenuPriorities.rightClickClipboard)]
        [MenuItem("Project Tools/Tools/Clipboard/Copy/All", false, MenuPriorities.clipboard)]
        //Copy all game objects in the game
        static void CopyAll() //Create a prefab of all objects in game in resource/clipboard folder
        {
            ClearClipboard();             
            allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();    //Store array of all root objects 
            MakePrefabs(allObjects, clipboardPath, allObjects.Length);  //Make prefabs of individual objects            
        }
        [MenuItem("GameObject/Clipboard/Cut/All", false, MenuPriorities.rightClickClipboard)]
        [MenuItem("Project Tools/Tools/Clipboard/Cut/All", false, MenuPriorities.clipboard)]
        static void CutAll() //Same as copy but delete all the objects copied
        {            
            cut = true;            
            ClearClipboard();  //Empty the clipboard
            allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();    //Store array of all root objects
            MakePrefabs(allObjects, clipboardPath, allObjects.Length);  //Make prefabs of individual objects
            DeleteAll();
        }
        [MenuItem("GameObject/Clipboard/Paste/To Current Scene", false, MenuPriorities.rightClickClipboard)]
        [MenuItem("Project Tools/Tools/Clipboard/Paste/To Current Scene %#v", false, MenuPriorities.clipboard)]
        static void PasteAll() //Instantiate all objects in resource folder
        {
            GameObject clipboardObject;  //Make an empty object to instantiate into so we can manipulate the empty objects name
            string clone = "(Clone)"; //String to replace
            Object[] clipboard = Resources.LoadAll("Clipboard");  //Load the clipboard
            for (int x = 0; x < clipboard.Length; x++)   //Loop through the clipboard
            {
                clipboardObject = Instantiate((GameObject)clipboard[x]); //Instantiate clipboard object
                clipboardObject.name = clipboardObject.name.Replace(clone, ""); //Replace (Clone) in the name with nothing
                if (SelectionHandler.GetSelected() != null)
                {
                    clipboardObject.transform.parent = SelectionHandler.GetSelected().transform;
                } 
            }
            CreateNewLevel.SaveScene();
        }
        [MenuItem("GameObject/Clipboard/Cut/Selected", false, MenuPriorities.rightClickClipboard)]
        [MenuItem("Project Tools/Tools/Clipboard/Cut/Selected %#x", false, MenuPriorities.clipboard)]
        static void CutSelected() //Same as copy but delete all the objects copied
        {           
            cut = true;     //We have cut            
            ClearClipboard();  //Empty the clipboard            
            //SelectionHandler.DeselectChildren();    //Deselect child objects 
            allObjects = Selection.gameObjects;     //Array of game objects is equal to selection of all game objects 
            
            MakePrefabsSelected(allObjects, clipboardPath, allObjects.Length);  //Make prefabs of individual objects
            DeleteAll();  //Cut objects out
        }
        [MenuItem("GameObject/Clipboard/Copy/Selected", false, MenuPriorities.rightClickClipboard)]
        [MenuItem("Project Tools/Tools/Clipboard/Copy/Selected %#c", false, MenuPriorities.clipboard)]
        static void CopySelected() //Copy all selected objects to resources/clipboard
        {
            ClearClipboard();
            //SelectionHandler.DeselectChildren();
            allObjects = Selection.gameObjects; //Store array of objects
            MakePrefabsSelected(allObjects, clipboardPath, allObjects.Length);  //Make prefabs of individual objects
        }
        [MenuItem("GameObject/Clipboard/Paste/To New Level", false, MenuPriorities.rightClickClipboard)]
        [MenuItem("Project Tools/Tools/Clipboard/Paste/To New Level", false, MenuPriorities.levels)]
        static void PasteNewLevel() //Copy clipboard
        {            
            CreateNewLevel.AddNewLevel();
            PasteAll();
        }            
        public static void ClearClipboard() //Recursively delete directory and recreate
        {
            //Delete the directory, true indicates recursive meaning to delete all subfiles and directories        
            Directory.Delete(clipboardPath, true);
            //Then recreate it
            Directory.CreateDirectory(clipboardPath);
            //Refresh the database for updated results
            AssetDatabase.Refresh();
        }  
        static void DeleteAll() //Delete all game objects (used in cut)
        {
            for (int x = 0; x < allObjects.Length; x++)
            {
                DestroyImmediate(allObjects[x].gameObject);
            }
        }
        static void RenameDuplicates(int x) //Adds " (clone)" to any existing object name
        {
            // GameObject fileCheck = (GameObject)AssetDatabase.LoadAssetAtPath(clipboardPath + allObjects[x].name, typeof(GameObject));
            Object fileCheck = (Object)AssetDatabase.LoadAssetAtPath(clipboardPath + allObjects[x].name + ".prefab", typeof(Object));
            //While the path already exists
            while (fileCheck)
            {
                allObjects[x].name = allObjects[x].name + "(clone)";  //Rename object
                fileCheck = (Object)AssetDatabase.LoadAssetAtPath(clipboardPath + allObjects[x].name + ".prefab", typeof(Object)); //Check again
            }
        }
    }
}
