using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollectScript : MonoBehaviour {
    [SerializeField]
    AddingScore addingScore;
    [SerializeField]
    string color;

    // Use this for initialization
    void Awake () {
        addingScore = GameObject.Find("Score").GetComponent<AddingScore>();
        addingScore.AddGem(color,1);
	}

}
