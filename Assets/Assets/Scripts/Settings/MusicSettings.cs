using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("audioSettings"))
        {
            PauseMenus.audioSettings = (PauseMenus.AudioSettings)System.Enum.Parse(typeof(PauseMenus.AudioSettings), PlayerPrefs.GetString("audioSettings"));
            switch (PauseMenus.audioSettings)
            {
                case PauseMenus.AudioSettings.Mono:
                    gameObject.GetComponent<AudioHighPassFilter>().enabled = true;
                    break;
                case PauseMenus.AudioSettings.Stereo:
                    gameObject.GetComponent<AudioHighPassFilter>().enabled = false;
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
