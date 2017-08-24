//Written By Christopher Cooke
//Gem Quest Gem Pool
//Gathers gems from directory and allows game objects to be spawned as copies at random
using System.Collections.Generic;
using UnityEngine;

public class gemPool : MonoBehaviour
{

    //Private Variables
    [SerializeField]
    //List<GameObject> gems;
    Object[] gems;

    //Properties
    public Object[] Gems { get { return gems; } }

    //Methods
    public void LoadGemsAtPath(string path) //Set gems list
    {
        Object[] gemObjects = Resources.LoadAll(path, typeof(GameObject));
        int objectCount = 0;

        if (gemObjects.Length > 0)
        {
            gems = new GameObject[gemObjects.Length];

            foreach (Object gem in gemObjects)
            {
                gems[objectCount] = ((GameObject)gem);
                objectCount++;

            }
        }
        else
        {
            Debug.Log("Directory contained no Game Objects");
        }
    }
    public Object ReturnMatchingPrefab(Object currentBase)
    {
        if(gems.Length > 0)
        {
            foreach(Object gem in gems)
            {
                if(currentBase == gem)
                {
                    return gem;
                }
            }
        }
        return null;
    }
    public Object GetRandomGem(Transform parent)
    {
        int count = gems.Length;
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
