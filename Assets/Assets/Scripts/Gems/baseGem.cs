//Written By Christopher Cooke
//Gem Quest Base Gem Class
//Contains all the most basic funtionality of a gem
//Pre & Post Destroy must be overridden
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class baseGem : MonoBehaviour  
{ 
    //Public Variables
    [SerializeField]
    public GameObject UpgradedPrefab;


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
    public void DestroyGem()    //Includes pre and post
    {
        PreDestroy();
        Destroy();
        PostDestroy();
    }       
   
    void Destroy()  //Destroy gem objects gem game object
    {
        DestroyImmediate(gemGO);
    }
    public abstract void PreDestroy();
    public abstract void PostDestroy();
    
}
