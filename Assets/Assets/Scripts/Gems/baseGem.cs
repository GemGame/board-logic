﻿//Written By Christopher Cooke
//Gem Quest Base Gem Class
//Contains all the most basic funtionality of a gem
//Pre & Post Destroy must be overridden
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public Animator mySprite;


    //Private Variables
    [SerializeField]
    public GameObject basePrefab;
    [SerializeField]
    GameObject gemGO;

    //Properties
    public GameObject GemGO { get { return gemGO; } set { gemGO = value; } }
    public int X { get { return (int)transform.position.x; } }
    public int Y { get { return (int)transform.position.y; } }

    //Methods
    public void SetGemProperties(Vector3 position, GameObject gem)   //Use before spawning
    {
        gemGO = gem;
    }
    public GameObject SpawnGemCopy(Transform parent, GameObject gemPrefab, GameObject baseGemPrefab)  //Spawns Gem Game Object
    {
        basePrefab = baseGemPrefab;
        return Instantiate(gemPrefab, parent.position, parent.rotation, parent);
    }
    public IEnumerator DestroyGem(bool isCombo)    //Includes pre and post
    {
        if (isCombo)
            yield return new WaitForSeconds(2.0f);
        PreDestroy();
        PassScript();
        Destroy();
        PostDestroy();
        Sound.sound.PlayOneShot(explosionSound, PauseMenus.SFXvolume);

    }

    void PassScript()
    {
        GameObject newGameObject = new GameObject("Temp GO");
        newGameObject.transform.position = this.transform.position;
        defaultGem newGemScript = newGameObject.AddComponent<defaultGem>();
        newGemScript = (defaultGem)this;
    }

    void Destroy()  //Destroy gem objects gem game object
    {
        DestroyImmediate(gemGO);
    }
    public abstract void PreDestroy();
    public abstract void PostDestroy();
    public abstract void UpgradeGem();
}