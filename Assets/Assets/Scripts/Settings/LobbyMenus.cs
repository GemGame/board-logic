using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenus : MonoBehaviour
{
    enum CurMenu { Basic, Options, Audio, Video };
    private CurMenu curMenu;
    [SerializeField]
    Transform options;
    [SerializeField]
    AudioClip hover;
    [SerializeField]
    AudioClip click;
    [SerializeField]
    AudioSource au;
    [SerializeField]
    Transform aud;
    [SerializeField]
    Transform vid;

    public void Pop()
    {
        au.pitch = .6f;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
    
    }
    public void Whoop()
    {
        au.pitch = .6f;
        au.PlayOneShot(hover, PauseMenus.SFXvolume);
    }

    public void Options()
    {
        au.pitch = 1f;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
        GameObject myObject = GameObject.Find("Canvas/Menus/Basic/Options");
        myObject.GetComponent<Animator>().enabled = false;
        myObject.GetComponent<Text>().raycastTarget = false;
        myObject = GameObject.Find("Canvas/Menus/Basic/Leaderboards");
        myObject.GetComponent<Text>().raycastTarget = false;
        myObject = GameObject.Find("Canvas/Menus/Basic/Credits");
        myObject.GetComponent<Text>().raycastTarget = false;
        curMenu = CurMenu.Options;
        GetComponent<Animator>().Play("ToOptions",0,0);
        Animator title = GameObject.Find("Canvas/Menus/GameTitle").GetComponent<Animator>();
        title.Play("FlippyFlippy");
        Animator anim = GameObject.Find("Canvas/Background").GetComponent<Animator>();
        anim.speed = 1;
        anim.Play("ToRed", 0, 0);
        BasicMenusScript.canSelect = false;
    }

    public void Return()
    {
        au.pitch = 1f;
        switch (curMenu)
        {
            case CurMenu.Options:
                curMenu = CurMenu.Basic;            
                au.PlayOneShot(click, PauseMenus.SFXvolume);
                Animator anim = GameObject.Find("Canvas/Background").GetComponent<Animator>();
                anim.speed = 1;
                anim.Play("FromRed", 0, 0);
                StartCoroutine(FromOptions());
                break;
            case CurMenu.Audio:
                au.PlayOneShot(click, PauseMenus.SFXvolume);
                GetComponent<Animator>().Play("FromAudio", 0, 0);
                curMenu = CurMenu.Options;
                break;
            case CurMenu.Video:
                au.PlayOneShot(click, PauseMenus.SFXvolume);
                GetComponent<Animator>().Play("FromVideo", 0, 0);
                curMenu = CurMenu.Options;
                break;
        }
    }
        IEnumerator FromOptions()
    {
        GetComponent<Animator>().Play("FromOptions", 0, 0);
        while (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime != 0)
            yield return null;
        while(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            yield return null;
        GameObject myObject = GameObject.Find("Canvas/Menus/Basic/Options");
        myObject.GetComponent<Animator>().enabled = true;
        myObject = GameObject.Find("Canvas/Menus/Basic/Options");
        myObject.GetComponent<Text>().raycastTarget = true;
        myObject = GameObject.Find("Canvas/Menus/Basic/Leaderboards");
        myObject.GetComponent<Text>().raycastTarget = true;
        myObject = GameObject.Find("Canvas/Menus/Basic/Credits");
        myObject.GetComponent<Text>().raycastTarget = true;
        BasicMenusScript.canSelect = true;
    }

    public void Audio()
    {
        if (curMenu == CurMenu.Options)
        {
            au.pitch = 1f;
            GetComponent<Animator>().Play("ToAudio", 0, 0);
            GameObject.Find("Canvas/Menus/Basic/Options/Audio").GetComponent<Animator>().enabled = false;
            GameObject.Find("Canvas/Menus/Basic/Options/Audio").GetComponent<Text>().raycastTarget = false;
            curMenu = CurMenu.Audio;
            Animator title = GameObject.Find("Canvas/Menus/GameTitle").GetComponent<Animator>();
            title.Play("FlippyFlippy");
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/Settings").GetComponent<Text>().text = PauseMenus.audioSettings.ToString();
            GameObject.Find("Canvas/Menus/Basic/Options/Audio").GetComponent<Transform>().localScale = new Vector3(1,1,1);
        }
    }
    public void Video()
    {
        if (curMenu == CurMenu.Options)
        {
            au.pitch = 1f;
            GetComponent<Animator>().Play("ToVideo", 0, 0);
            GameObject.Find("Canvas/Menus/Basic/Options/Video").GetComponent<Animator>().enabled = false;
            GameObject.Find("Canvas/Menus/Basic/Options/Video").GetComponent<Text>().raycastTarget = false;
            curMenu = CurMenu.Video;
            Animator title = GameObject.Find("Canvas/Menus/GameTitle").GetComponent<Animator>();
            title.Play("FlippyFlippy");
        }
    }

    //change difficulty
    public void Diff()
    {
        au.pitch = 1.0f;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
        switch (PauseMenus.difficulty)
            {
                default:
                PauseMenus.difficulty = PauseMenus.Difficulty.Normal;
                break;
                case PauseMenus.Difficulty.Normal:
                PauseMenus.difficulty = PauseMenus.Difficulty.Hard;
                    break;
                case PauseMenus.Difficulty.Hard:
                PauseMenus.difficulty = PauseMenus.Difficulty.Easy;
                    break;
            }
        PlayerPrefs.SetString("difficulty", PauseMenus.difficulty.ToString());
        GameObject.Find("Canvas/Menus/Basic/Options/Difficulty").GetComponent<Text>().text = PauseMenus.difficulty.ToString();
    }

    //change audio Settigs
    public void Mono()
    {
        au.pitch = 1.0f;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
        switch (PauseMenus.audioSettings)
        {
            default:
                PauseMenus.audioSettings = PauseMenus.AudioSettings.Stereo;
                GameObject.Find("Game Manager/Sound").GetComponent<AudioHighPassFilter>().enabled = false;
                GameObject.Find("Game Manager/Music").GetComponent<AudioHighPassFilter>().enabled = false;
                break;
            case PauseMenus.AudioSettings.Stereo:
                PauseMenus.audioSettings = PauseMenus.AudioSettings.Mono;
                GameObject.Find("Game Manager/Sound").GetComponent<AudioHighPassFilter>().enabled = true;
                GameObject.Find("Game Manager/Music").GetComponent<AudioHighPassFilter>().enabled = true;
                break;
        }
        PlayerPrefs.SetString("audioSettings", PauseMenus.audioSettings.ToString());
        GameObject.Find("Canvas/Menus/Basic/Options/Audio/Settings").GetComponent<Text>().text = PauseMenus.audioSettings.ToString();
    }

    //change GameSpeed
    public void GameSpeed()
    {
        au.pitch = 1.0f;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
        switch (PauseMenus.gameSpeed)
        {
            default:
                PauseMenus.gameSpeed = PauseMenus.GameSpeed.Normal;
                PauseMenus.scaleTime = 1.2f;
                break;
            case PauseMenus.GameSpeed.Normal:
                PauseMenus.gameSpeed = PauseMenus.GameSpeed.Fast;
                PauseMenus.scaleTime = 1.4f;
                break;
            case PauseMenus.GameSpeed.Fast:
                PauseMenus.gameSpeed = PauseMenus.GameSpeed.Fastest;
                PauseMenus.scaleTime = 1.6f;
                break;
            case PauseMenus.GameSpeed.Fastest:
                PauseMenus.gameSpeed = PauseMenus.GameSpeed.Slowest;
                PauseMenus.scaleTime = .8f;
                break;
            case PauseMenus.GameSpeed.Slowest:
                PauseMenus.gameSpeed = PauseMenus.GameSpeed.Slow;
                PauseMenus.scaleTime = 1f;
                break;
        }
        Time.timeScale = PauseMenus.scaleTime;
        PlayerPrefs.SetString("gameSpeed", PauseMenus.gameSpeed.ToString());
        GameObject.Find("Canvas/Menus/Basic/Options/GameSpeed").GetComponent<Text>().text = PauseMenus.gameSpeed.ToString();
    }

    public void Higlight()
    {
        if (curMenu == CurMenu.Options)
        {
            au.pitch = 1f;
            Color c = Color.white;
            c.a = .8f;
            au.PlayOneShot(hover, PauseMenus.SFXvolume);
            foreach (Transform item in options)
            {
                item.gameObject.GetComponent<Text>().color = c;
                item.gameObject.GetComponent<Animator>().enabled = false;
            }
            c.a = 1f;
            UnityEngine.EventSystems.EventSystem myEvent = GameObject.Find("Canvas/EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
            myEvent.currentSelectedGameObject.GetComponent<Text>().color = c;
            myEvent.currentSelectedGameObject.GetComponent<Animator>().enabled = true;
            if (myEvent.currentSelectedGameObject.name == "GameSpeed")
            {
                myEvent.currentSelectedGameObject.GetComponent<Text>().text = PauseMenus.gameSpeed.ToString();
                GameObject.Find("Canvas/Menus/Basic/Options/Difficulty").GetComponent<Text>().text = "Difficulty";
            }
            else if (myEvent.currentSelectedGameObject.name == "Difficulty")
            {
                myEvent.currentSelectedGameObject.GetComponent<Text>().text = PauseMenus.difficulty.ToString();
                GameObject.Find("Canvas/Menus/Basic/Options/GameSpeed").GetComponent<Text>().text = "Game Speed";
            }
            else
            {
                GameObject.Find("Canvas/Menus/Basic/Options/GameSpeed").GetComponent<Text>().text = "Game Speed";
                GameObject.Find("Canvas/Menus/Basic/Options/Difficulty").GetComponent<Text>().text = "Difficulty";
            }
        }
        else
            return;
    }
    //highlighting one of the audio menus
    public void AudioHighlight()
    {
        au.pitch = 1f;
        Color c = Color.white;
        c.a = .8f;
        au.PlayOneShot(hover, PauseMenus.SFXvolume);
        foreach (Transform item in aud)
        {
            item.gameObject.GetComponent<Text>().color = c;
            item.gameObject.GetComponent<Animator>().enabled = false;
            item.gameObject.transform.localScale = new Vector3(1,1,1);
        }
        c.a = 1f;
        UnityEngine.EventSystems.EventSystem myEvent = GameObject.Find("Canvas/EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        myEvent.currentSelectedGameObject.GetComponent<Animator>().enabled = true;
        myEvent.currentSelectedGameObject.GetComponent<Text>().color = c;
        myEvent.currentSelectedGameObject.GetComponent<Animator>().enabled = true;

        if (myEvent.currentSelectedGameObject.name == "SFXVolume")
        {
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/SFXVolume").GetComponent<Text>().text = "<" + Mathf.Round(PauseMenus.SFXvolume * 10).ToString() + ">";
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/BGMVolume").GetComponent<Text>().text = "BGM Volume";
        }
        else if (myEvent.currentSelectedGameObject.name == "BGMVolume")
        {
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/BGMVolume").GetComponent<Text>().text = "<" + Mathf.Round(PauseMenus.BGMvolume * 10).ToString() + ">";
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/SFXVolume").GetComponent<Text>().text = "SFX Volume";
        }
        else
        {
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/SFXVolume").GetComponent<Text>().text = "SFX Volume";
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/BGMVolume").GetComponent<Text>().text = "BGM Volume";
        }
    }

    //highlighting one of the video menus
    public void VideoHighlight()
    {
        au.pitch = 1f;
        Color c = Color.white;
        c.a = .8f;
        au.PlayOneShot(hover, PauseMenus.SFXvolume);
        foreach (Transform item in vid)
        {
            item.gameObject.GetComponent<Text>().color = c;
            item.gameObject.GetComponent<Animator>().enabled = false;
            item.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        c.a = 1f;
        UnityEngine.EventSystems.EventSystem myEvent = GameObject.Find("Canvas/EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        myEvent.currentSelectedGameObject.GetComponent<Animator>().enabled = true;
        myEvent.currentSelectedGameObject.GetComponent<Text>().color = c;
        myEvent.currentSelectedGameObject.GetComponent<Animator>().enabled = true;

        if (myEvent.currentSelectedGameObject.name == "Brightness")
        {

            GameObject.Find("Canvas/Menus/Basic/Options/Video/Brightness").GetComponent<Text>().text = "<" + Mathf.Round(10 - (PauseMenus.brightness * 10)).ToString() + ">";
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Quality").GetComponent<Text>().text = "Quality";
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Resolution").GetComponent<Text>().text = "Resolution";
        }
        else if (myEvent.currentSelectedGameObject.name == "Quality")
        {
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Brightness").GetComponent<Text>().text = "Brightness";
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Resolution").GetComponent<Text>().text = "Resolution";
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Quality").GetComponent<Text>().text = "<"+PauseMenus.quality.ToString()+">";
        }
        else if (myEvent.currentSelectedGameObject.name == "Resolution")
        {
            if (PlayerPrefs.HasKey("resolution"))
                PauseMenus.resolution = (PauseMenus.Resolution)System.Enum.Parse(typeof(PauseMenus.Resolution), PlayerPrefs.GetString("resolution"));
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Brightness").GetComponent<Text>().text = "Brightness";
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Quality").GetComponent<Text>().text = "Quality";
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Resolution").GetComponent<Text>().text = "<"+PauseMenus.resolution.ToString()+">";
        }
        else
        {
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Brightness").GetComponent<Text>().text = "Brightness";
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Quality").GetComponent<Text>().text = "Quality";
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Resolution").GetComponent<Text>().text = "Resolution";
        }
    }

    public void BGMVolumeUp()
    {
        if (PauseMenus.BGMvolume < 1)
        {
            au.pitch = 1f;
            au.PlayOneShot(click, PauseMenus.SFXvolume);
            PauseMenus.BGMvolume += .1f;
            if (PauseMenus.BGMvolume > 1f)
                PauseMenus.BGMvolume = 1f;
            PlayerPrefs.SetFloat("BGMvolume", PauseMenus.BGMvolume);
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/BGMVolume").GetComponent<Text>().text = "<" + Mathf.Round(PauseMenus.BGMvolume * 10).ToString() + ">";
            GameObject.Find("Game Manager/Music").GetComponent<AudioSource>().volume = PauseMenus.BGMvolume;
        }
    }
    public void BGMVolumeDown()
    {
        if (PauseMenus.BGMvolume > 0)
        {
            au.pitch = 1f;
            PauseMenus.BGMvolume -= .1f;
            if (PauseMenus.BGMvolume < 0)
                PauseMenus.BGMvolume = 0;
            PlayerPrefs.SetFloat("BGMvolume", PauseMenus.BGMvolume);
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/BGMVolume").GetComponent<Text>().text = "<" + Mathf.Round(PauseMenus.BGMvolume * 10).ToString() + ">";
            GameObject.Find("Game Manager/Music").GetComponent<AudioSource>().volume = PauseMenus.BGMvolume;
            au.PlayOneShot(click, PauseMenus.SFXvolume);
        }
    }

    public void SFXVolumeUp()
    {
        if (PauseMenus.SFXvolume < 1)
        {
            au.pitch = 1f;
            PauseMenus.SFXvolume += .1f;
            if (PauseMenus.SFXvolume > 1)
                PauseMenus.SFXvolume = 1;
            PlayerPrefs.SetFloat("SFXvolume", PauseMenus.SFXvolume);
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/SFXVolume").GetComponent<Text>().text = "<" + Mathf.Round(PauseMenus.SFXvolume * 10).ToString() + ">";
            GameObject.Find("Game Manager/Sound").GetComponent<AudioSource>().volume = PauseMenus.BGMvolume;
            au.PlayOneShot(click, PauseMenus.SFXvolume);
        }
    }
    public void SFXVolumeDown()
    {
        if (PauseMenus.SFXvolume > 0)
        {
            au.pitch = 1f;
            PauseMenus.SFXvolume -= .1f;
            if (PauseMenus.SFXvolume < 0)
                PauseMenus.SFXvolume = 0;
            PlayerPrefs.SetFloat("SFXvolume", PauseMenus.SFXvolume);
            GameObject.Find("Canvas/Menus/Basic/Options/Audio/SFXVolume").GetComponent<Text>().text = "<" + Mathf.Round(PauseMenus.SFXvolume * 10).ToString() + ">";
            GameObject.Find("Game Manager/Sound").GetComponent<AudioSource>().volume = PauseMenus.BGMvolume;
            au.PlayOneShot(click, PauseMenus.SFXvolume);
        }
    }
    public void BrightnessDown()
    {
        if (PauseMenus.brightness < .8f)
        {
            au.pitch = 1f;
            PauseMenus.brightness += .1f;
            if (PauseMenus.brightness > .8f)
                PauseMenus.brightness = .8f;
            PlayerPrefs.SetFloat("brightness", PauseMenus.brightness);
            Color c = Color.black;
            c.a = PauseMenus.brightness;
            GameObject.Find("Canvas/Image").GetComponent<Image>().color = c;
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Brightness").GetComponent<Text>().text = "<" + Mathf.Round(10 -(PauseMenus.brightness * 10)).ToString() + ">";
            GameObject.Find("Game Manager/Sound").GetComponent<AudioSource>().volume = PauseMenus.BGMvolume;
            au.PlayOneShot(click, PauseMenus.SFXvolume);
        }
    }
    public void BrightnessUp()
    {
        if (PauseMenus.brightness > 0)
        {
            au.pitch = 1f;
            au.PlayOneShot(click, PauseMenus.SFXvolume);
            PauseMenus.brightness -= .1f;
            if (PauseMenus.brightness < 0)
                PauseMenus.brightness = 0;
            PlayerPrefs.SetFloat("brightness", PauseMenus.brightness);
            Color c = Color.black;
            c.a = PauseMenus.brightness;
            GameObject.Find("Canvas/Image").GetComponent<Image>().color = c;
            GameObject.Find("Canvas/Menus/Basic/Options/Video/Brightness").GetComponent<Text>().text = "<" + Mathf.Round(10 - (PauseMenus.brightness * 10)).ToString() + ">";
            GameObject.Find("Game Manager/Sound").GetComponent<AudioSource>().volume = PauseMenus.BGMvolume;
        }
    }

    //change resolution
    public void ChangeResolution()
    {
        au.pitch = 1f;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
        switch (PauseMenus.resolution)
        {
            default:
                Screen.SetResolution(600, 400, false);
                PauseMenus.resolution = PauseMenus.Resolution.Small;
                break;
            case PauseMenus.Resolution.Small:
                Screen.SetResolution(800, 600, false);
                PauseMenus.resolution = PauseMenus.Resolution.Medium;
                break;
            case PauseMenus.Resolution.Medium:
                Screen.SetResolution(Screen.width, Screen.height, true);
                PauseMenus.resolution = PauseMenus.Resolution.Fullscreen;
                break;
        }
        GameObject.Find("Canvas/Menus/Basic/Options/Video/Resolution").GetComponent<Text>().text = "<" + PauseMenus.resolution.ToString() + ">";
        PlayerPrefs.SetString("resolution", PauseMenus.resolution.ToString());
    }

    //change quality
    public void ChangeQuality()
    {
        au.pitch = 1f;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
        switch (PauseMenus.quality)
        {
            default:
                PauseMenus.quality = PauseMenus.Quality.Good;
                QualitySettings.SetQualityLevel(1);
                break;
            case PauseMenus.Quality.Good:
                PauseMenus.quality = PauseMenus.Quality.High;
                QualitySettings.SetQualityLevel(2);
                break;
            case PauseMenus.Quality.High:
                PauseMenus.quality = PauseMenus.Quality.Poor;
                QualitySettings.SetQualityLevel(0);
                break;
        }
        GameObject.Find("Canvas/Menus/Basic/Options/Video/Quality").GetComponent<Text>().text = "<" + PauseMenus.quality.ToString() + ">";
        PlayerPrefs.SetString("quality", PauseMenus.quality.ToString());
    }
}
