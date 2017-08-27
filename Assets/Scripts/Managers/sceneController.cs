using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    public int scenesUntilFirstLevel = 1;
    AsyncOperation ao;
    bool sceneCurrentlyLoading = false;    
    bool freshScene = true;
    
    void Awake()
    {
        DisableDestroyOnLoad();
    }   
    void Update()
    {
        if (freshScene && !IsStartingScene())
        {
            //Debug.Log("Attempting to load scene " + GetNextScene(GetCurrentSceneIndex()));
            //PrepareNextSceneAsync();
        }
        else if(Input.GetKeyDown(KeyCode.Space))    //No loading when it hasn't had the chance to prep
        {
            //Debug.Log(TryLoadPreparedScene());
            //freshScene = true;
        }
    }

    //Public Controls -- Make Sure to prepare a scene before loading
    public IEnumerator PrepareAndLoadCurrentScene()
    {
        if (!sceneCurrentlyLoading)
        {
            PrepareCurrentLevelAsynch();
        }

        while (!TryLoadPreparedScene())
        {
            yield return null;
        }
    }
    public IEnumerator PrepareAndLoadNextScene(bool arcadeMode)
    {

        if (!sceneCurrentlyLoading)
        {
            if (!arcadeMode)
            {
                if (GetCurrentSceneIndex() < scenesUntilFirstLevel)
                {
                    PrepareFirstLevelAsynch();
                }
                else
                {
                    PrepareNextSceneAsync();
                }
            }
            else 
            {
                PrepareNextSceneAsync();
            }
        }
        yield return null;// new WaitForSeconds(3.0f);  //Can insert wait for seconds to leave a load screen up for example
        while (!TryLoadPreparedScene())
        {
            yield return null;
        }
    }
    public IEnumerator PrepareAndLoadMainMenu()
    {
        if (!sceneCurrentlyLoading)
        {
            PrepareMainMenuAsynch();
        }
        while (!TryLoadPreparedScene())
        {
            yield return null;
        }
    }
    public bool TryLoadPreparedScene()  //Try to launch the background load
    {
        if (ao.progress == 0.9f)    //Async Operation Completed
        {
            freshScene = true;
            ao.allowSceneActivation = true;
            return true;
        }
        return false;
    }
    public void PrepareFirstLevelAsynch()
    {
        if (!sceneCurrentlyLoading)
        {
            sceneCurrentlyLoading = true;
            StartCoroutine(PrepareSceneAsync(scenesUntilFirstLevel));
        }
    }
    public void PrepareCurrentLevelAsynch()
    {
        if (!sceneCurrentlyLoading)
        {
            sceneCurrentlyLoading = true;
            StartCoroutine(PrepareSceneAsync(GetCurrentSceneIndex()));
        }
    }
    public void LoadFirstLevel()    //Non asynch -- Called from main menu until level select set up
    {
        freshScene = true;
        SceneManager.LoadScene(scenesUntilFirstLevel);
    }    

    //Scene Loading   
    IEnumerator PrepareSceneAsync(int buildIndex)   //Background scene loader
    {
        yield return null;

        ao = SceneManager.LoadSceneAsync(buildIndex);
        ao.allowSceneActivation = false;
        bool finished = false;
        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            //Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f && !finished)
            {
                finished = true;
                Debug.Log("Scene " + buildIndex + " Done Loading");
                //ao.allowSceneActivation = true;                
            }
            yield return null;
        }
        sceneCurrentlyLoading = false;
    }
    void PrepareNextSceneAsync()    //Based off current scene index
    {
        freshScene = false;
        StartCoroutine(PrepareSceneAsync(GetNextScene(GetCurrentSceneIndex())));
    }
    void LoadSceneByName(string name)   //Non asynch
    {
        SceneManager.LoadScene(name);
    }
    void LoadSceneByIndex(int sceneIndex)   //Non asynch
    {
        SceneManager.LoadScene(sceneIndex);
    }   

    //Helper Methods
    bool IsStartingScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return true;
        return false;
    }
    int GetNextScene(int currentScene)
    {
        const int defaultScene = 0;
        int numScenes = SceneManager.sceneCountInBuildSettings;
        int potentialNextScene = currentScene + 1;

        if (potentialNextScene < numScenes && potentialNextScene > scenesUntilFirstLevel)
        {
            return potentialNextScene;
        }
        else if (potentialNextScene < scenesUntilFirstLevel)
        {
            return defaultScene + 1;
        }
        else
        {
            EnableDestroyOnLoad();
            return defaultScene;
        }
    }    
    int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    void DisableDestroyOnLoad()
    {
        DontDestroyOnLoad(this);
    }
    void EnableDestroyOnLoad()
    {
        GameObject tempParent = new GameObject("Score Manager Time Bomb");
        this.transform.SetParent(tempParent.transform);
    }    

    //Unused Methods
    void PrepareRandomAsyncScene()  //Unused
    {
        int numScenes = SceneManager.sceneCountInBuildSettings;
        int rndLevelIndex = Random.Range(scenesUntilFirstLevel, numScenes);
        // Debug.Log("Chose scene " + rndLevelIndex + " out of " + numScenes + "scenes.");
        if (!sceneCurrentlyLoading)
        {
            sceneCurrentlyLoading = true;
            StartCoroutine(PrepareSceneAsync(rndLevelIndex));
        }
    }
    void PrepareMainMenuAsynch()    //Background load scene 0 -- Unused
    {
        if (!sceneCurrentlyLoading)
        {
            sceneCurrentlyLoading = true;
            StartCoroutine(PrepareSceneAsync(0));
        }
    }
    void PrepareRandomSceneFromMainMenu()   //Asynchronous -- Unused
    {
        if (!sceneCurrentlyLoading)
        {
            if (IsStartingScene())
            {
                PrepareRandomAsyncScene();
            }
        }
    }    
}

