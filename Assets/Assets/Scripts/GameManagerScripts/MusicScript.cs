using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class MusicScript : MonoBehaviour
{
    [SerializeField]
    AudioClip[] music;
    public static AudioSource auSource;
    [SerializeField]
    int curMusic;

    private void Start()
    {
        auSource = gameObject.GetComponent<AudioSource>();
        curMusic = Random.Range (0, music.Length);
        auSource.clip = music[curMusic];
        auSource.Play();
        StartCoroutine(LerpMusic());

    }

    IEnumerator LerpMusic()
    {
        while (auSource.volume < PauseMenus.BGMvolume)
        {
            auSource.volume += .01f;
            if (auSource.volume > PauseMenus.BGMvolume)
                auSource.volume = PauseMenus.BGMvolume;
            yield return new WaitForSeconds(.1f);
        }
    }

    public IEnumerator MusicOff()
    {
        while (auSource.volume > 0)
        {
            auSource.volume -= .01f;
            if (auSource.volume < 0)
                auSource.volume = 0;
            yield return new WaitForSeconds(.01f);
        }
    }
}
