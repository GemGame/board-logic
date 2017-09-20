//Written By Christopher Cooke
//Gem Quest Base Gem Class
//Contains all the most basic funtionality of a gem
//Pre & Post Destroy must be overridden
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

[System.Serializable]
public abstract class baseGem : MonoBehaviour
{
    //Public Variables
    [SerializeField]
    public GameObject upgradeEffectPrefab;
    [SerializeField]
    public GameObject UpgradedPrefab;
    [SerializeField]
    public GameObject explosionPrefab;
    [SerializeField]
    public GameObject floatingTextPrefab;
    [SerializeField]
    public Text floatingTextValue;
    public int value = 100;
    [SerializeField]
    public AudioClip explosionSound;
    [SerializeField]
    public AudioClip upgradeSound;
    [SerializeField]
    public Animator mySprite; //on the prefab, this is disabled by default
    [SerializeField]
    public GemSelect gemSelect;
    private static bool playSound;
    public int runningCor = 0; //the amount of corouties that are running



    //Private Variables
    [SerializeField, HideInInspector]
    public Object basePrefab;
    [SerializeField, HideInInspector]
    GameObject gemGO;

    //Properties
    public GameObject GemGO { get { return gemGO; } set { gemGO = value; } }
    public int X { get { return (int)transform.position.x; } }
    public int Y { get { return (int)transform.position.y; } }

    //Methods
    private void Start()
    {
        runningCor = 0;
    }

    public void SetGemProperties(Vector3 position, GameObject gem, Transform parent)   //Use before spawning
    {
        gemGO.transform.parent = parent;
        gemGO = gem;
    }
    public GameObject SpawnGemCopy(Transform parent, GameObject gemPrefab, Object baseGemPrefab)  //Spawns Gem Game Object
    {
        basePrefab = baseGemPrefab;
        //GameObject tempGO = (GameObject)PrefabUtility.InstantiatePrefab(gemPrefab);
        //tempGO.GetComponent<baseGem>().SetGemProperties(Vector3.zero, tempGO, parent);
        mySprite.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        return Instantiate(gemPrefab, parent.position, parent.rotation, parent);
    }
    public IEnumerator DestroyGem(bool isCombo)    //Includes pre and post
    {
        if (isCombo)
        {
            try
            {
                if (runningCor == 0)
                    Sound.sound.PlayOneShot(upgradeSound, PauseMenus.SFXvolume);
            }
            catch
            {

            }
            yield return null;//new WaitForSeconds(2.0f)
        }
        else
            PostDestroy();

        PreDestroy();
        DisableGameObject();
        Destroy();
    }
    void DisableGameObject()
    {
        MeshRenderer[] mr = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in mr)
        {
            r.enabled = false;
        }
    }

    void Destroy()  //Destroy gem objects gem game object
    {
        if(!Application.isPlaying)
            DestroyImmediate(gemGO);
    }
    public abstract void PreDestroy();
    public abstract void PostDestroy();
    public abstract void UpgradeGem();
    public abstract void HintGem();
}