//Written By Christopher Cooke
//Gem Quest Gem Pool
//Gathers gems from directory and allows game objects to be spawned as copies at random
using System.Collections.Generic;
using UnityEngine;

public class gemPool : MonoBehaviour
{

    //Private Variables
    [SerializeField, HideInInspector]
    List<GameObject> gems;

    //Properties
    public List<GameObject> Gems { get { return gems; } }

    //Methods
    public void LoadGemsAtPath(string path) //Set gems list
    {
        gems = new List<GameObject>();
        Object[] gemObjects = Resources.LoadAll(path, typeof(GameObject));

        if (gemObjects.Length > 0)
        {
            foreach (Object gem in gemObjects)
            {
                gems.Add((GameObject)gem);
            }
        }
        else
        {
            Debug.Log("Directory contained no Game Objects");
        }
    }
    public GameObject GetRandomGem(Transform parent)
    {
        int count = gems.Count;
        if (count > 0)
        {
            int randomIndex = Random.Range(0, count);
            return gems[randomIndex];
        }
        else
        {
            return null;
        }
    }
}
