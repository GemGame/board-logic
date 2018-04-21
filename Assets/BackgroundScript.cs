using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<Transform> list = new List<Transform>();
        foreach (Transform child in transform)
        {
            list.Add(child);
        }
        int r = Random.Range(0,list.Count);
        list[r].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
