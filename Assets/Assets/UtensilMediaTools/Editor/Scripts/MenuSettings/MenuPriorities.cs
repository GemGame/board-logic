//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016

namespace EditorTools
{
    //Contains constant priority values for different menu's in every class
    public class MenuPriorities 
    {
        public const int autosave = 0;
        public const int light = -100;
        public const int folders = 2;
        public const int levels = 13;
        public const int clipboard = 13;
        public const int tagging = 13;
        public const int material = 42;
        public const int prefabs = 48;
        public const int rightClickTransform = 0;
        public const int rightClickTagging = -1;
        public const int rightClickClipboard = -200;
        public const int rightClickFolders = 30;
        public const int lineage = 25;
        public const int selection = 0;
    }
}