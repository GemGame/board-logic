using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class baseGem : MonoBehaviour //: ScriptableObject 
{

    //Public Variables
    [SerializeField]
    public GameObject UpgradedPrefab;
    //Private Variables
    [SerializeField]   
    GameObject gemGO;

    //[SerializeField]
    //public static int uniqueID = 0;

    //Properties
    public GameObject GemGO { get { return gemGO; } set { gemGO = value; } }
    public int X { get { return (int)transform.position.x; } }
    public int Y { get { return (int)transform.position.y; } }

    private void Start()
    {
        //Debug.Log(uniqueID++);
        //gemGO = this.gameObject;
    }
   

    public void SetGemProperties(Vector3 position, GameObject gem)   //Use before spawning
    {
       // location = position;
        gemGO = gem;
        //Debug.Log(gemGO);
    }
    public GameObject SpawnGemCopy(Transform parent, GameObject gemPrefab)  //Spawns Gem Game Object
    {
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
        //Debug.Log("Destroying gem at position " + gemGO.transform.position.x + ", " + gemGO.transform.position.y);
        DestroyImmediate(gemGO);
        //Debug.Log(gemGO);
    }
    public abstract void PreDestroy();
    public abstract void PostDestroy();
    
}
