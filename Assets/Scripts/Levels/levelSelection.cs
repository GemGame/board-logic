using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelSelection : MonoBehaviour {

    sceneController sc;
    inputManager im;
    public level[] levels;
    public bool clearPlayerPrefs = false;

    // Use this for initialization
    private void Awake()
    {
        if (clearPlayerPrefs)
        {
            PlayerPrefs.DeleteAll();  // -- Use with caution
        }
        levels = GetAllLevels();
        DetermineLevelLockedState();
        UnlockNextLevel();

    }
    void Start () {
        if (GameObject.Find("SceneManager"))
        {
            sc = GameObject.Find("SceneManager").GetComponent<sceneController>();
        }
        //im = this.gameObject.AddComponent<inputManager>();
	}
	
	// Update is called once per frame
	public void LoadLevel(int selection)    //Called in Canvas/Panel/Level OnClick()
    {
        Debug.Log(selection);
        if(sc != null)
        {
            if (selection != -1 && GetAllLevels()[selection - 1].unlocked)
            {
                StartCoroutine(sc.PrepareAndLoadLevelNumber(selection));
                
            }
        }
	}
    void UnlockNextLevel()
    {
        for(int x = 0; x < levels.Length; x++)
        {
            if (levels[x].unlocked && x + 1 < levels.Length && !levels[x+1].unlocked)
            {
                Debug.Log((x + 1) + " was set to true and is level number " + levels[x+1].levelNumber);
                levels[x+1].unlocked = true;
                DisableLockChild(levels[x]);
                level.SetLevelUnlocked(x, true);
                break;
            }
        }
        level.SetLevelUnlocked(0, true);
        levels[0].unlocked = true;

    }
    void DetermineLevelLockedState()
    {
        foreach(level l in levels)
        {
            l.unlocked = l.GetLevelUnlocked(l.levelNumber.ToString());  //Player prefs bool version
            l.gameObject.GetComponent<Animator>().enabled = true;
            DisableLockChild(l);
        }
    }
    void DisableLockChild(level level)
    {
        Transform[] children = level.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject.name == "Lock" && level.unlocked)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    public level[] GetAllLevels()
    {
        GameObject[] levelObjects = GameObject.FindGameObjectsWithTag("Level");
        level[] levels = new level[levelObjects.Length];
        for(int x = 0; x < levelObjects.Length; x++)
        {
            int index = levelObjects[x].GetComponent<level>().levelNumber - 1;
            levels[index] = levelObjects[x].GetComponent<level>();
        }
       
        return levels;
    }
}
