//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;

namespace EditorTools
{
    public class SceneAutoSaveConfiguration : EditorWindow
    {
        string apply = "Apply";
        string cancel = "Cancel";
        string defaults = "Restore Defaults";
        static bool autoSaveEnabled;
        static int intervalMinutes;
        static int intervalHours;
        static int numSaveCopies;
        
        [MenuItem("Project Tools/Editor Settings/Configure Autosave", false, MenuPriorities.autosave)]
        public static void ShowWindow()
        {
            autoSaveEnabled = SceneAutoSave.AutoSaveEnabled;
            intervalHours = SceneAutoSave.IntervalHours;
            intervalMinutes = SceneAutoSave.IntervalMinutes;
            numSaveCopies = SceneAutoSave.NumSaveCopies;
            EditorWindow.GetWindow(typeof(SceneAutoSaveConfiguration)); //Show existing window instance. If one does not exist, make one.
        }

        void OnGUI()
        {
            GUILayout.Label("Autosave Settings", EditorStyles.boldLabel);
            autoSaveEnabled = EditorGUILayout.BeginToggleGroup("Enabled", autoSaveEnabled);
            intervalHours = EditorGUILayout.IntSlider("Interval Hours", intervalHours, 0, 23);
            intervalMinutes = EditorGUILayout.IntSlider("Interval Minutes", intervalMinutes, 1, 59);
            numSaveCopies = EditorGUILayout.IntSlider("Number of scene copies", numSaveCopies, 1, 10);             
            EditorGUILayout.EndToggleGroup();

            if (GUILayout.Button(apply))    //If user clicks apply button
            {
                //Save editor preferences                
                SceneAutoSave.AutoSaveEnabled = autoSaveEnabled;
                SceneAutoSave.NumSaveCopies = numSaveCopies;
                SceneAutoSave.IntervalHours = intervalHours;
                SceneAutoSave.IntervalMinutes = intervalMinutes;

                //Update the time interval
                SceneAutoSave.UpdateTimeInterval(SceneAutoSave.IntervalHours, SceneAutoSave.IntervalMinutes);   //Update the time interval

                Debug.Log("Auto-Save configuration successfully updated.");

                this.Close();   //Close the window
            }
            else if (GUILayout.Button(cancel))  //If user clicks cancel button
            {                
                Debug.Log("Auto-Save configuration update cancelled by user.");
                this.Close();
            }
            else if (GUILayout.Button(defaults))
            {
                //Restore editor preferences to default const values               
                SceneAutoSave.AutoSaveEnabled = SceneAutoSave.autoSaveEnabled;
                SceneAutoSave.NumSaveCopies = SceneAutoSave.numSaveCopies;
                SceneAutoSave.IntervalHours = SceneAutoSave.intervalHours;
                SceneAutoSave.IntervalMinutes = SceneAutoSave.intervalMinutes;

                Debug.Log("Autosave default values restored.");
                ShowWindow();
            }
        }       
    }
}