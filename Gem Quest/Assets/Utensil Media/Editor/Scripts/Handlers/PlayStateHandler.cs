//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEditor;

namespace EditorTools {
    [InitializeOnLoad]
    public static class PlayStateHandler 
    {
        static PlayStateHandler()
        {
            EditorApplication.playmodeStateChanged += ModeChanged;            
        }
        static void ModeChanged()
        {
            //In case you would like to use this tool to do more things, here are the different states

            // Scene is stopped         :     isPlayingOrWillChangePlaymode = false;  isPlaying = false
            // Pressed Playback button  :     isPlayingOrWillChangePlaymode = true;   isPlaying = false
            // Pressed Stop Button      :     isPlayingOrWillChangePlaymode = false;  isPlaying = true
            // Scene is playing         :     isPlayingOrWillChangePlaymode = true;   isPlaying = true

            if (EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying)         //Scene is playing
            {                                   
            }
            else if (!EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying)   //Pressed stop button
            {             
            }
            else if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)   //Pressed play button
            {                
            }
            else if (!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)  //Scene is stopped
            {              
                ObjectClipboard.AfterExit();                
            }
        }
        public static bool GetPlaying() //Return isPlaying boolean
        {
            bool isPlaying = true;
            if(!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)   //If scene is stoped
            {
                isPlaying = false;                
            }
            return isPlaying;
        }
       
    }
}
