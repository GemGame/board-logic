using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    AudioClip _gemSelect;
    public static AudioClip gemSelect;
    public static AudioSource sound;
    static int index;//the number of times the sound has played
    IEnumerator myWait;

    static public Sound instance;

    void Start()
    {
        instance = this;
        gemSelect = _gemSelect;
        sound = gameObject.GetComponent<AudioSource>();
        myWait = instance.Wait();
    }

    public static void SelectGem()
    {
        if (index < 2)
        {//preventing the sound from playing more than 10 times. Otherwise, it will cause an issue where the sounds dies for a short period.
            sound.pitch = 1.4f;
            sound.PlayOneShot(gemSelect, PauseMenus.SFXvolume);
            index++;
            instance.StopAllCoroutines();
            instance.StartCoroutine(instance.Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.05f);
        index = 0;
    }
}
