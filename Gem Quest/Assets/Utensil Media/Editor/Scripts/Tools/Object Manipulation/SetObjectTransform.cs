//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;

//This class makes a number of menu items and sets different selections of gameobjects to vector3.zero

namespace EditorTools {
    [ExecuteInEditMode]
    public class SetObjectTransform 
    {
        [MenuItem("GameObject/Reset Transform", false, MenuPriorities.rightClickTransform)]
        [MenuItem("GameObject/Reset Transform/Reset Global Transform", false, MenuPriorities.selection)]
        [MenuItem("Project Tools/Game Object/Reset Global Transform", false, MenuPriorities.selection)]
        public static void SetNeutralTransformSelected()
        {
            GameObject[] sgo = Selection.gameObjects;
            foreach (GameObject go in sgo)
            {
                go.transform.position = Vector3.zero;
            }
        }
        [MenuItem("GameObject/Reset Transform/Reset Lineage Global Transform", false, MenuPriorities.lineage)]
        [MenuItem("Project Tools/Game Object/Reset Lineage Global Transform", false, MenuPriorities.lineage)]
        public static void SetNuetralTransformSelectedLineage()
        {
            GameObject[] sgo = Selection.gameObjects;
            foreach (GameObject go in sgo)
            {
                Component[] children = SelectionHandler.GetLineage(go);
                go.transform.position = Vector3.zero;
                foreach (Transform child in children)
                {
                    child.transform.position = Vector3.zero;
                }

            }
        }
        [MenuItem("GameObject/Reset Transform/Reset Local Transform", false, MenuPriorities.selection)]
        [MenuItem("Project Tools/Game Object/Reset Local Transform", false, MenuPriorities.selection)]
        public static void SetNeutralLocalTransformSelected()
        {
            GameObject[] sgo = Selection.gameObjects;
            foreach (GameObject go in sgo)
            {
                go.transform.localPosition = Vector3.zero;
            }
        }
        [MenuItem("GameObject/Reset Transform/Reset Lineage Local Transform", false, MenuPriorities.lineage)]
        [MenuItem("Project Tools/Game Object/Reset Lineage Local Transform", false, MenuPriorities.lineage)]
        public static void SetNuetralLocalTransformSelectedLineage()
        {
            GameObject[] sgo = Selection.gameObjects;
            foreach (GameObject go in sgo)
            {
                Component[] children = SelectionHandler.GetLineage(go);
                go.transform.position = Vector3.zero;
                foreach (Transform child in children)
                {
                    child.transform.localPosition = Vector3.zero;
                }

            }
        }
        [MenuItem("GameObject/Reset Transform/Reset Lineage Local Transform - Exclude Parent", false, MenuPriorities.lineage)]
        [MenuItem("Project Tools/Game Object/Reset Lineage Local Transform - Exlcude Parent", false, MenuPriorities.lineage)]
        public static void SetNuetralLocalTransformOnlySelectedLineage()
        {
            GameObject[] sgo = Selection.gameObjects;
            foreach (GameObject go in sgo)
            {
                Component[] children = SelectionHandler.GetLineage(go);
                // go.transform.position = Vector3.zero;
                for (int i = 1; i < children.Length; i++)
                {
                    children[i].transform.localPosition = Vector3.zero;
                }

            }
        }
        
        
    }
}
