//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Globalization;
using System;


namespace EditorTools
{
    
    [InitializeOnLoad]
    [ExecuteInEditMode]
    public class SceneAutoSave {
        //Public Variables
        //Default const values for Editor Preferences
        public const bool autoSaveEnabled = true;
        public const int intervalMinutes = 15;      //Default time interval in minutes
        public const int intervalHours = 0;        //Hours
        public const int numSaveCopies = 2;        //Default number of save files for each scene        

        //Private Variables
        private const string Save_Folder = "Scenes/Autosaves/";     //Autosave directory
        private const string levelPath = @"Assets\Scenes\Levels\";  //Level Directory    
        private static System.TimeSpan updateInterval;
        private static int numLevels;
        private static System.DateTime lastSaveTime = System.DateTime.Now;
        private static FileSystemInfo directoryFiles;       
        private static int stepCounter = 0;
        private static bool playedMessage = false;

        //Properties        
        public static int IntervalMinutes
        {
            get { return EditorPrefs.GetInt("IntervalMinutes", intervalMinutes); }
            set { EditorPrefs.SetInt("IntervalMinutes", value); }
        }
        public static int IntervalHours
        {
            get { return EditorPrefs.GetInt("IntervalHours", intervalHours); }
            set { EditorPrefs.SetInt("IntervalHours", value); }
        }
        public static int NumSaveCopies
        {
            get { return EditorPrefs.GetInt("NumSaveCopies", numSaveCopies); }
            set { EditorPrefs.SetInt("NumSaveCopies", value); }
        }
        public static bool AutoSaveEnabled
        {
            get { return EditorPrefs.GetBool("AutoSaveEnabled", autoSaveEnabled); }
            set { EditorPrefs.SetBool("AutoSaveEnabled", value); }
        }


        static SceneAutoSave()
        {            
            EnsureAutoSavePathExists();
            RegisterOnEditorUpdate(IntervalHours, IntervalMinutes); //Change this number to modify autosave interval
        }
        public static int GetFileCount(string directory)
        {
            return Directory.GetFiles(directory).Length;    //Get a file count in a given directory
        }
       
        public static void DeleteOldestFile(string directory)   //Delete the oldest file in a directory
        {
            int indexOfOldestCreated = 0;   //Default
            string[] files = Directory.GetFiles(directory); //Get all file name in directory
            DateTime oldestCreation = File.GetCreationTime(files[0]);   //Default
            for (int i = 0; i < files.Length; i++)
            {
                if(File.GetCreationTime(files[i]) < oldestCreation)     //If the file is older than the stored oldest
                {
                    indexOfOldestCreated = i;   //It is the new oldest file
                }
            }
            File.Delete(files[indexOfOldestCreated]);   //Delete the oldest file
        }
        public static void LimitFileCount(string directory)     //Limit the number of autosave files
        {
            while (GetFileCount(directory) > NumSaveCopies * 2)    //While the directory has more files than allowed - 2 files per autosave
            {               
                DeleteOldestFile(directory);
            }
        }
        private static void EnsureAutoSavePathExists()  //Make sure the target save path exists
        {
           // Debug.Log("Ensure PAth");

            var path = Path.Combine(Application.dataPath, Save_Folder);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void RegisterOnEditorUpdate(int hours, int minutes)   //Only call in the constructor. Assigns default values and adds onto update
        {
           
                UpdateTimeInterval(hours, minutes);
            
            EditorApplication.update += OnUpdate;
        }
        public static void UpdateTimeInterval(int hours, int minutes)   //Set update time interval
        {
            updateInterval = new TimeSpan(hours, minutes, 0);
        }
        private static void SaveScene()     //Save the currently open scene
        {
                //StepBackInterval(); //If the interval was stepped up, step it back  
                Debug.Log("Auto saving scene: " + EditorSceneManager.GetActiveScene().name);
                Debug.Log("Saved at : " + DateTime.Now);

                EnsureAutoSavePathExists(); //Ensure the path exists

                // Get the new saved scene name.
                var newName = GetNewSceneName(EditorSceneManager.GetActiveScene().name);    //Get the file name
                var folder = Path.Combine("Assets", Save_Folder);
                folder += GetSceneFolder(EditorSceneManager.GetActiveScene().name);     //Get the folder path

                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), Path.Combine(folder, newName), true);     //Autosave scene
                LimitFileCount(folder);     //Delete old files in directory

                //EditorApplication.SaveScene(Path.Combine(folder, newName), true);
                AssetDatabase.SaveAssets();
                
        }

        private static string GetSceneFolder(string originalSceneName)      //Get the path of a given scene's folder
        {
            var scene = Path.GetFileNameWithoutExtension(originalSceneName);

            return scene;
        }
        private static string GetNewSceneName(string originalSceneName)     //Rename scene to add date and time onto it (autosave file title)
        {
           // Debug.Log("Get Scene Name");

            var scene = Path.GetFileNameWithoutExtension(originalSceneName);

            return string.Format(
                "{0}_{1}.unity",
                scene,  //0
                System.DateTime.Now.ToString("MM-dd_HH-mm-ss", CultureInfo.InvariantCulture)); //1
        }

        private static void OnUpdate()  //Added onto Editor.Update in the constructor
        {
           
            if (System.DateTime.Now - lastSaveTime >= updateInterval)   //If the time interval has passed
            {
                if (AutoSaveEnabled && !PlayStateHandler.GetPlaying())    //And saving is enabled
                {
                    SaveScene();    //Save the scene
                    lastSaveTime = System.DateTime.Now;     //Update last save time
                    playedMessage = false;
                }
                else
                {
                    if (playedMessage == false)
                    {
                        Debug.Log("Autosave delayed pending exit of playmode : " + DateTime.Now);
                        playedMessage = true;
                    }
                }

                AssetDatabase.Refresh();    //Refresh the database
            }
        }
        private static void StepInterval()
        {
            TimeSpan stepMinute = new TimeSpan(0, 1, 0);
            updateInterval += stepMinute;
            stepCounter++;
        } 
        private static void StepBackInterval()
        {
            TimeSpan stepMinute = new TimeSpan(0, 1, 0);
            for (int i = 0; i < stepCounter; i++)
            {
                updateInterval -= stepMinute;
            }
        }
       
    }
}
