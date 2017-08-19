using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Animator>().Play("PanelIntro",0,0);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
