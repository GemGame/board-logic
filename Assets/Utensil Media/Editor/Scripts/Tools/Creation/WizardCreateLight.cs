//Developed By : Christopher Cooke
//Unity3D Beginner's Editor Toolbox
//12/19/2016
using UnityEngine;
using UnityEditor;


namespace EditorTools {
    public class WizardCreateLight : ScriptableWizard
    {
        //Wizard Fields
        public float range = 500;
        public Color color = Color.white;
        public LightType lightType;
        


        [MenuItem("GameObject/Light/Create Light Wizard", false, MenuPriorities.light)]
        [MenuItem("Project Tools/Tools/Create/Create Light Wizard", false, MenuPriorities.light)]
        static void CreateWizard()
        {
            //ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create", "Apply");
            //If you don't want to use the secondary button simply leave it out:
            ScriptableWizard.DisplayWizard<WizardCreateLight>("Create Light", "Create");
        }

        void OnWizardCreate()
        {
            GameObject go = new GameObject(lightType.ToString() + " Light");    //Create new light gameobject
            Light lt = go.AddComponent<Light>();    //Attach the light
            //Configure light
            lt.type = lightType;
            lt.range = range;
            lt.color = color;

        }

        void OnWizardUpdate()
        {
            helpString = "Configure new scene light!";
        }

        /*
        // When the user pressed the "Apply" button OnWizardOtherButton is called.
        void OnWizardOtherButton()
        {
            if (Selection.activeTransform != null)
            {
                Light lt = Selection.activeTransform.GetComponent<Light>();

                if (lt != null)
                {
                    lt.color = Color.red;
                }
            }
        }
         */
    }
}