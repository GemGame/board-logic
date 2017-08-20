using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{
    public int excludedLevelIndexCount = 1;
    AsyncOperation ao;
    bool coroutineRunning = false;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(TryLoadAsyncScene());
        }
        CheckCoroutineStart();
    }

    void CheckCoroutineStart()
    {
        if (!coroutineRunning)
        {
            if (IsStartingScene())
            {
                PrepareRandomAsyncScene();
            }
            else
            {
                PrepareMainMenuAsynch();
            }
        }
    }
    bool IsStartingScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < excludedLevelIndexCount) return true;
        return false;
    }
    public bool TryLoadAsyncScene()
    {
        if (ao.progress == 0.9f)
        {
            ao.allowSceneActivation = true;
            return true;
        }
        return false;
    }
    void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    void PrepareMainMenuAsynch()
    {
        if (!coroutineRunning)
        {
            coroutineRunning = true;
            StartCoroutine(PrepareSceneAsync(0));
        }
    }
    void PrepareRandomAsyncScene()
    {
        int numScenes = SceneManager.sceneCountInBuildSettings;
        int rndLevelIndex = Random.Range(excludedLevelIndexCount, numScenes);
        // Debug.Log("Chose scene " + rndLevelIndex + " out of " + numScenes + "scenes.");
        if (!coroutineRunning)
        {
            coroutineRunning = true;
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
            // Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                // Debug.Log("Scene " + buildIndex + " Done Loading");
                //ao.allowSceneActivation = true;
            }
            yield return null;
        }
        coroutineRunning = false;
    }
}

