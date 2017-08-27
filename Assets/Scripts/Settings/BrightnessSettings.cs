using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("brightness"))
        {
            Color myColor = gameObject.GetComponent<Image>().color;
            myColor.a = PlayerPrefs.GetFloat("brightness");
            gameObject.GetComponent<Image>().color = myColor;
        }
    }
}
