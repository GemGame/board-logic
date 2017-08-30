
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

public class level : MonoBehaviour {

    public int levelNumber;
    public bool unlocked = false;
    [HideInInspector]
    public Button levelButton;

    private void Start()
    {
        levelButton = this.GetComponent<Button>();
        //ConfigureEnabledColliders();
    }

    private void Update()
    {
        
    }
    void ConfigureEnabledColliders()
    {
        if (!unlocked)
        {
            GetComponent<BoxCollider>().enabled = false;            
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }
    
    public static void SetLevelUnlocked(int levelNumber, bool state)     //0 = false || 1 == true
    {
        string level = "Level" + levelNumber.ToString();
        PlayerPrefs.SetInt(level, state ? 1 : 0);        
        //EditorPrefs.SetBool(level, state);        
    }

    public bool GetLevelUnlocked(string levelNumber)
    {
        string level = "Level" + levelNumber;
        int value = PlayerPrefs.GetInt(level);
        //bool editorValue = EditorPrefs.GetBool(level);

        if (value == 1)
        {
            return true;
        }
        
        else
        {
            return false;
        }
    }

}
