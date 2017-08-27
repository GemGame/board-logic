using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    [SerializeField]
    AudioClip _gemSelect;
    public static AudioClip gemSelect;
    public static AudioSource sound;

    void Start()
    {
        gemSelect = _gemSelect;
        sound = gameObject.GetComponent<AudioSource>();
    }

	public static void SelectGem()
    {
        sound.pitch = 1.4f;
        sound.PlayOneShot(gemSelect, PauseMenus.SFXvolume);
    }
}
