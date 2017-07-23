﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class gemPool : MonoBehaviour {

    //Private Variables
    [SerializeField, HideInInspector]
    List<GameObject> gems;

    //Properties
    public List<GameObject> Gems { get { return gems; } }
    
    public void LoadGemsAtPath(string path) //Set gems list
    {
        gems = new List<GameObject>();
        Object[] gemObjects = Resources.LoadAll(path, typeof(GameObject)); //Resources.LoadAll(path);
        
        foreach (Object gem in gemObjects)
        {
            Debug.Log("Gem added : " + gem.name);
            gems.Add((GameObject)gem);
        }
    }

    public GameObject GetRandomGem(Transform parent)
    {
        int randomIndex = Random.Range(0, gems.Count);
        //Debug.Log(gems[randomIndex]);
        //return Instantiate(gems[randomIndex], parent);
        return gems[randomIndex];//.GetComponent<baseGem>();
    }
}
