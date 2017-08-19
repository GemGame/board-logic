//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEditor;
using UnityEngine;
using System.Collections;

namespace EditorTools
{
    public class TagOffspring 
    {
        //Inspector Variables

        //Private Variables
        static GameObject parent;   
        static string tag;
        [MenuItem("GameObject/Tags/", false, MenuPriorities.rightClickTagging)]
        [MenuItem("GameObject/Tags/Tag Children", false, MenuPriorities.rightClickTagging)]
        [MenuItem("Project Tools/Tools/Tags/Tag Children", false, MenuPriorities.tagging)]
        static void TagChildrenOfParent()
        {            
            AssignTag(SelectionHandler.ListChildren(SelectionHandler.GetSelected()));  //Assign tags to the list of children
            Debug.Log("Assigned tag '" + SelectionHandler.GetSelected().tag + "' to immediate children of " + SelectionHandler.GetSelected());
        }
        [MenuItem("GameObject/Tags/Tag Lineage", false, MenuPriorities.rightClickTagging)]
        [MenuItem("Project Tools/Tools/Tags/Tag Lineage", false, MenuPriorities.tagging)]
        static void TagLineageOfParent()    //Recursively tag all children of selected game object
        {
             //Set parent object          
            foreach(Component t in SelectionHandler.GetLineage(SelectionHandler.GetSelected()))
            {
                
                t.gameObject.tag = SelectionHandler.GetSelected().tag;
            }
            Debug.Log("Assigned tag '" + SelectionHandler.GetSelected().tag + "' to entire lineage of " + SelectionHandler.GetSelected());
            
        }
       
        
        //Assign tag to game object
        static void AssignTag(GameObject[] children)    //Assign tags to array of child gameobjects
        {
            for (int x = 0; x < children.Length; x++)   //For every child
            {
                children[x].tag = SelectionHandler.GetSelected().tag;  //Assign the tag
            }
        }
    }
}
