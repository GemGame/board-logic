//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;

namespace EditorTools
{
    public class CreateNewLevel : MonoBehaviour
    {        
        static string path = @"Assets\Scenes\Levels\";
        static string levelName = "Level_";       
        static string extension = ".unity";
        static string levelPath = @"Assets/Utensil Media/Editor/Resources/Level Template/";
        
        [MenuItem("Project Tools/Project/Add New Level/Empty Level", false, MenuPriorities.levels)]
        public static void AddNewLevel()
        {            
            //Store new scene information
            NewSceneSetup setup = NewSceneSetup.EmptyScene;
            NewSceneMode mode = NewSceneMode.Single;
            //Add a 0 in the name if less then 10
            if (GetLevelIndex() < 10)
            {
                Debug.Log(path + levelName + "0" + GetLevelIndex() + extension);
                //Create and save a new scene
                EditorSceneManager.SaveScene(EditorSceneManager.NewScene(setup, mode), path + levelName + "0" + GetLevelIndex() + extension);
            }
            else //Else just do it normally without adding a 0
            {
                Debug.Log(path + levelName + GetLevelIndex() + extension);
                EditorSceneManager.SaveScene(EditorSceneManager.NewScene(setup, mode), path + levelName + GetLevelIndex() + extension);
            }
            

            AssetDatabase.Refresh();
        }
        [MenuItem("Project Tools/Project/Add New Level/From Template", false, MenuPriorities.levels)]
        public static void AddTemplateLevel()
        {
            AddNewLevel();
            Object templateObject;  //Make an empty object to instantiate into so we can manipulate the empty objects name
            string clone = "(Clone)"; //String to replace
            Object[] template = Resources.LoadAll("Level Template");  //Load the clipboard
            for (int x = 0; x < template.Length; x++)   //Loop through the clipboard
            {
                templateObject = Instantiate(template[x]); //Instantiate clipboard object
                templateObject.name = templateObject.name.Replace(clone, ""); //Replace (Clone) in the name with nothing                                                                                 
            }
            CreateNewLevel.SaveScene();
        }
       
        static int GetLevelIndex()
        {
            string[] files = Directory.GetFiles(path);
            PlayerPrefs.SetInt("levelNumber", Mathf.FloorToInt((float)files.Length / 2) + 1);
            return PlayerPrefs.GetInt("levelNumber");
        }
        public static void SaveScene()
        {
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        }        
        [MenuItem("Project Tools/Project/Restructure Levels", false, MenuPriorities.levels)]
        static void RenameLevels()
        {
            string[] files = Directory.GetFiles(path);
            
            for (int x = 0; x < files.Length; x += 2)
            {
                if (Mathf.FloorToInt(((float)x / 2) + 1) < 10)  //Adds a 0 to level numbers under 10
                {
                    System.IO.File.Move(files[x], path + levelName + "0" + Mathf.FloorToInt(((float)x / 2) + 1) + extension);
                }
                else
                {
                    System.IO.File.Move(files[x], path + levelName + Mathf.FloorToInt(((float)x / 2) + 1) + extension);
                }
            }
            AssetDatabase.Refresh();
        }
        [MenuItem("Project Tools/Project/Create Level Template/From All", false, MenuPriorities.levels)]
        static void CreateLevelTemplateAll()
        {
            ClearLevelTemplate();
            GameObject[] allObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();    //Store array of all root objects

            for (int x = 0; x < allObjects.Length; x++)  //Loop through all objects
            {
                Object fileCheck = (Object)AssetDatabase.LoadAssetAtPath(levelPath + allObjects[x].name + ".prefab", typeof(Object));
                //If the path already exists
                while (fileCheck)
                {
                    allObjects[x].name = allObjects[x].name + "(clone)";
                    fileCheck = (Object)AssetDatabase.LoadAssetAtPath(levelPath + allObjects[x].name + ".prefab", typeof(Object));
                }
                ObjectClipboard.MakePrefab(allObjects[x].transform.gameObject, levelPath);  //Make prefab of individual object
            }
        }
        [MenuItem("Project Tools/Project/Create Level Template/From Selected", false, MenuPriorities.levels)]
        static void CreateLevelTemplateSelected()
        {
            ClearLevelTemplate();
            //SelectionHandler.DeselectChildren();
            GameObject[] allObjects = Selection.gameObjects;
            for (int x = 0; x < allObjects.Length; x++)  //Loop through all objects
            {
                Object fileCheck = (Object)AssetDatabase.LoadAssetAtPath(levelPath + allObjects[x].name + ".prefab", typeof(Object));
                //If the path already exists
                while (fileCheck)
                {
                    allObjects[x].name = allObjects[x].name + "(clone)";
                    fileCheck = (Object)AssetDatabase.LoadAssetAtPath(levelPath + allObjects[x].name + ".prefab", typeof(Object));
                }
                ObjectClipboard.MakePrefab(allObjects[x].transform.gameObject, levelPath);  //Make prefab of individual object
            }
        }
        
        public static void ClearLevelTemplate()
        {
            //Recursively delete directory       
            Directory.Delete(levelPath, true);
            //Then recreate it
            Directory.CreateDirectory(levelPath);
            //Refresh the database for updated results
            AssetDatabase.Refresh();
        }  

    }
}
