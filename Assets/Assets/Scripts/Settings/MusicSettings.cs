using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettings : MonoBehaviour {
    static AudioSource au;
	// Use this for initialization
	void Start () {
        au = gameObject.GetComponent<AudioSource>();
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
            StartCoroutine(MusicSettings.TurnOnMusic());
        }
    }
	public static IEnumerator TurnOffMusic()
    {
        print("yes");
        while (au.volume > 0)
        {
            yield return new WaitForSeconds(.1f);
            au.volume -= .01f;
        }
    }

    public static IEnumerator TurnOnMusic()
    {
        while (au.volume < PauseMenus.BGMvolume)
        {
            yield return new WaitForSeconds(.1f);
            au.volume += .01f;
        }
    }
}
