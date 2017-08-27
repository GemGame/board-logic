//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;

namespace EditorTools
{
    public class SelectionHandler 
    {
        
        public static GameObject GetSelected()   //Set parent equal to the selected game object and tag equal to the parent's tag
        {
            GameObject go = Selection.activeGameObject;  //Store the active selection as the parent
            return go;
        }
        public static Object[] DeselectChildren()
        {
            Object[] selectedObjects = Selection.GetFiltered(typeof(GameObject), SelectionMode.TopLevel);   //Get a filtered selection of game objects as an object array
            Selection.objects = selectedObjects; //Make our selection equal to our object array            
            return selectedObjects;
        }
        public static GameObject[] GetParent()
        {
            return (GameObject[])Selection.GetFiltered(typeof(GameObject), SelectionMode.TopLevel);
        }
        public static GameObject[] ListChildren(GameObject parent)      //List all children of a selected parent
        {
            GameObject[] children;
            int numChildren;
            //Count the children of the parent
            numChildren = parent.transform.childCount;
           // Debug.Log(numChildren);
            //Set the array size equal to the number of children
            children = new GameObject[numChildren];
            //Assign each child to an array
            for (int x = 0; x < numChildren; x++)
            {
                children[x] = parent.transform.GetChild(x).gameObject;
            }
            return children;
        }
        public static Component[] GetLineage(GameObject parent)     //Get every transform in a lineage
        {
            Component[] lineage = parent.GetComponentsInChildren(typeof(Transform));
            return lineage;
        }




    }
}