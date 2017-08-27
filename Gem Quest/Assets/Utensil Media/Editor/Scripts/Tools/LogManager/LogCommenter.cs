//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using System.IO;
using UnityEditor;

namespace EditorTools
{
    [ExecuteInEditMode]
    public class LogCommenter : EditorWindow
    {
        static string commentActive = "Comment Active Logs", deleteActive = "Delete Active Logs", 
            deleteInactive = "Delete Inactive Logs", commentInactive = "Make All Active", deleteAll = "Delete All Logs";
        static bool commentAllActive = false;
        static bool commentAllInactive = false;
        static bool deleteAllDebug = false;
        static bool deleteAllActiveDebug = false;
        static bool deleteAllInactiveDebug = false;
        static bool fixAllNewLines = false;
        static string scriptFolderPath = @"Assets\Scripts\";
        static int numFiles;
        static string[] fileNames;
        
        [MenuItem("Project Tools/Tools/Log Commenter")]
        static void ShowWindow()    //Display editor window and set bool values to false
        {
            commentAllActive = false;
            commentAllInactive = false;
            deleteAllDebug = false;
            deleteAllActiveDebug = false;
            deleteAllInactiveDebug = false;
            fixAllNewLines = false;
            GetFileNames();
            EditorWindow.GetWindow(typeof(LogCommenter));
        }
        void OnGUI()    //Contains editor window labels and buttons
        {
            GUILayout.Label("Debug Log Manager", EditorStyles.boldLabel);
            
            if(GUILayout.Button("Remove Empty Lines"))
            {
                fixAllNewLines = true;
                Debug.Log("Removing empty lines from files.");
                ReadAllFiles();
                this.Close();
            }            
            else if (GUILayout.Button(commentInactive))
            {
                commentAllInactive = true;
                Debug.Log("Making all inactive Debug.Log lines active.");
                ReadAllFiles();
                this.Close();
            }
            else if (GUILayout.Button(commentActive))    
            {
                commentAllActive = true;
                Debug.Log("Making all active Debug.Log lines inactive.");
                ReadAllFiles();
                this.Close();   
            }
            else if (GUILayout.Button(deleteAll))
            {
                deleteAllDebug = true;
                Debug.Log("Deleting all active & inactive Debug.Log lines.");
                ReadAllFiles();
                this.Close();
            }
            else if (GUILayout.Button(deleteActive))  
            {
                deleteAllActiveDebug = true;
                Debug.Log("Deleting all active Debug.Log lines.");
                ReadAllFiles();
                this.Close();
            }            
            else if (GUILayout.Button(deleteInactive))
            {
                deleteAllInactiveDebug = true;
                Debug.Log("Deleting all inactive Debug.Log lines.");
                ReadAllFiles();
                this.Close();
            }
            AssetDatabase.Refresh();
           
        }

        static void ReadAllFiles()  //Read each file in string array of file names
        {           
            foreach (string s in fileNames)
            {                
                ReadFile(s);              
            }
        }
        static void ReadFile(string path)   //Read a a file and act on that file based off of editor window selection
        {
            int counter = 0;
            string line;
            string allLines = "";
            // System.IO.Stream stream = new System.IO.Stream(path);
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            Debug.Log("Comment all Active :" + commentAllActive);
            while (file.Peek() != -1)
            {
                bool carriageReturn = true;
                line = file.ReadLine();

                if (fixAllNewLines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        //Debug.Log("Found null or empty line");
                        carriageReturn = false;
                        // line = DeleteLine(line);
                        line = null;
                    }
                }
                if (commentAllActive)
                {
                    // Debug.Log("Comment All Active Bool True");
                    if (IsActiveDebugLine(line))
                    {
                        //   Debug.Log("Found active line");

                        line = CommentLine(line);
                    }
                    //  Debug.Log(line);
                }
                if (commentAllInactive)
                {
                    if (IsInactiveDebugLine(line))
                        line = RemoveComments(line);
                }
                if (deleteAllActiveDebug)
                {
                    if (IsActiveDebugLine(line))
                    {
                        line = DeleteLine(line);
                    }
                }
                if (deleteAllInactiveDebug)
                {
                    if (IsInactiveDebugLine(line))
                    {
                        line = DeleteLine(line);
                    }
                }
                if (deleteAllDebug)
                {
                    if (IsActiveDebugLine(line) || IsInactiveDebugLine(line))
                    {
                        line = DeleteLine(line);
                    }
                }

                if (carriageReturn)
                    line += System.Environment.NewLine;

                if (line != null)
                    allLines += line;
            }
            file.Close();
            System.IO.StreamWriter writeFile = new System.IO.StreamWriter(path);

            writeFile.Write(allLines);
            writeFile.Flush();
            // Debug.Log(allLines);
            writeFile.Close();

            Debug.Log(counter);
        }
        static void GetFileNames()  //Recursively obtain all .cs files in Assets/Scripts and save their names in a string array
        {
            fileNames = new string[Directory.GetFiles(scriptFolderPath, "*.cs", SearchOption.AllDirectories).Length];
            fileNames =  Directory.GetFiles(scriptFolderPath, "*.cs", SearchOption.AllDirectories);
            foreach(string s in fileNames)
            {
               // Debug.Log(s);
            }
        }  
        
        static string DeleteLine(string line)   //Replace a line with an empty string
        {
            line = line.Replace(line, "");
            return line;
        }
        static string RemoveComments(string line) //Remove single line comments from a string
        {
            // Debug.Log("Removing Comments");
            if (line.Contains("//"))
            {
                line = line.Replace("//", "");
            }
            return line;
        }
        static string CommentLine(string line)  //Insert single line comments into a string
        {
            //   Debug.Log("Adding Comments to line : " + line);
            line = line.Insert(0, "//");
            //   Debug.Log(line);
            return line;
        }

        static bool IsActiveDebugLine(string line)  //If the line is a debug log that is not commented return true
        {
            bool debugLine = false;
            if(line.Contains("Debug.Log") && !line.Contains("//"))
            {
               // Debug.Log("Found an active debug line : " + line);
                debugLine = true;
            }
            return debugLine;
        }
        static bool IsInactiveDebugLine(string line)    //If the line is a debug log that is commented return true
        {
            bool debugLine = false;
            if (line.Contains("//") && line.Contains("Debug.Log"))
                if (line.IndexOf("//") < line.IndexOf("Debug.Log"))
                    debugLine = true;
            return debugLine;
        }  
    }
}