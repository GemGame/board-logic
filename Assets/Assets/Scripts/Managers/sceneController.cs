using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    public int scenesUntilFirstLevel = 1;
    AsyncOperation ao;
    bool sceneCurrentlyLoading = false;    
    public bool freshScene = true;
    public int currentScene = 0;
    // Use this for initialization
    void Awake()    //Keep all scenes
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (freshScene)
        {
            Debug.Log("Attempting to load scene " + GetNextScene());
            PrepareNextSceneAsync();
        }
        else if(Input.GetKeyDown(KeyCode.Space))    //No loading when it hasn't had the chance to prep
        {
            Debug.Log(TryLoadPreparedScene());
            freshScene = true;
        }
    }

   
    bool IsStartingScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return true;
        return false;
    }
    public bool TryLoadPreparedScene()
    {
        if (ao.progress == 0.9f)    //Async Operation Completed
        {
            freshScene = true;
            ao.allowSceneActivation = true;
            return true;
        }
        return false;
    }
    void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadFirstLevel()
    {
        currentScene = scenesUntilFirstLevel;
        freshScene = true;
        SceneManager.LoadScene(currentScene);
    }
    void PrepareMainMenuAsynch()
    {
        if (!sceneCurrentlyLoading)
        {
            sceneCurrentlyLoading = true;
            StartCoroutine(PrepareSceneAsync(0));
        }
    }
    void PrepareRandomSceneFromMainMenu()
    {
        if (!sceneCurrentlyLoading)
        {
            if (IsStartingScene())
            {
                PrepareRandomAsyncScene();
            }
        }
    }
    public void PrepareFirstLevelAsynch()
    {
        if (!sceneCurrentlyLoading)
        {
            sceneCurrentlyLoading = true;
            StartCoroutine(PrepareSceneAsync(scenesUntilFirstLevel));
            currentScene = scenesUntilFirstLevel;
        }
    } 
    void PrepareNextSceneAsync()
    {
        freshScene = false;
        currentScene = GetNextScene();
        StartCoroutine(PrepareSceneAsync(currentScene));
    }
    
    int GetNextScene()
    {
        const int defaultScene = 0;
        int numScenes = SceneManager.sceneCountInBuildSettings;
        int potentialNextScene = currentScene + 1;
        
        if (potentialNextScene < numScenes)
        {
            return potentialNextScene;
        }
        else
        {
            return defaultScene;
        }
    }
    void PrepareRandomAsyncScene()
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
    IEnumerator PrepareSceneAsync(int buildIndex)
    {
        yield return null;

        ao = SceneManager.LoadSceneAsync(buildIndex);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
             //Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                 Debug.Log("Scene " + buildIndex + " Done Loading");
                //ao.allowSceneActivation = true;
            }
            yield return null;
        }
        sceneCurrentlyLoading = false;
    }
}

