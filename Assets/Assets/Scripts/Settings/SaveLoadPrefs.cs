using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadPrefs : MonoBehaviour {

    //loading prefs
    public static void Load()
    {
        if (PlayerPrefs.HasKey("BGMvolume"))
            PauseMenus.BGMvolume = PlayerPrefs.GetFloat("BGMvolume");
        if (PlayerPrefs.HasKey("SFXvolume"))
            PauseMenus.SFXvolume = PlayerPrefs.GetFloat("SFXvolume");
        if (PlayerPrefs.HasKey("brightness"))
            PauseMenus.brightness = PlayerPrefs.GetFloat("brightness");
        if (PlayerPrefs.HasKey("scaleTime"))
            PauseMenus.scaleTime = PlayerPrefs.GetFloat("scaleTime");

        if (PlayerPrefs.HasKey("difficulty"))
            PauseMenus.difficulty = (PauseMenus.Difficulty)System.Enum.Parse(typeof(PauseMenus.Difficulty), PlayerPrefs.GetString("difficulty"));
        if (PlayerPrefs.HasKey("gameSpeed"))
        {
            PauseMenus.gameSpeed = (PauseMenus.GameSpeed)System.Enum.Parse(typeof(PauseMenus.GameSpeed), PlayerPrefs.GetString("gameSpeed"));
            switch (PauseMenus.gameSpeed)
            {
                case PauseMenus.GameSpeed.Slowest:
                    Time.timeScale = .8f;
                    break;
                case PauseMenus.GameSpeed.Slow:
                    Time.timeScale = 1;
                    break;
                case PauseMenus.GameSpeed.Normal:
                    Time.timeScale = 1.2f;
                    break;
                case PauseMenus.GameSpeed.Fast:
                    Time.timeScale = 1.4f;
                    break;
                case PauseMenus.GameSpeed.Fastest:
                    Time.timeScale = 1.6f;
                    break;
            }
            PauseMenus.scaleTime = Time.timeScale;
        }
        if (PlayerPrefs.HasKey("quality"))
            PauseMenus.quality = (PauseMenus.Quality)System.Enum.Parse(typeof(PauseMenus.Quality), PlayerPrefs.GetString("quality"));
        if (PlayerPrefs.HasKey("resolution"))
            PauseMenus.resolution = (PauseMenus.Resolution)System.Enum.Parse(typeof(PauseMenus.Resolution), PlayerPrefs.GetString("resolution"));
        if (PlayerPrefs.HasKey("audioSettings"))
            PauseMenus.audioSettings = (PauseMenus.AudioSettings)System.Enum.Parse(typeof(PauseMenus.AudioSettings), PlayerPrefs.GetString("audioSettings"));


        switch (PauseMenus.quality)
        {
            default:
                QualitySettings.SetQualityLevel(0);
                break;
            case PauseMenus.Quality.Good:
                QualitySettings.SetQualityLevel(1);
                break;
            case PauseMenus.Quality.High:
                QualitySettings.SetQualityLevel(2);
                break;
        }

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
    }

    //saving all data
    public static void Save()
    {
        PlayerPrefs.SetFloat("BGMvolume", PauseMenus.BGMvolume);
        PlayerPrefs.SetFloat("SFXvolume", PauseMenus.SFXvolume);
        PlayerPrefs.SetFloat("brightness", PauseMenus.brightness);
        PlayerPrefs.SetFloat("scaleTime", PauseMenus.scaleTime);

        PlayerPrefs.SetString("difficulty", PauseMenus.difficulty.ToString());
        PlayerPrefs.SetString("gameSpeed", PauseMenus.gameSpeed.ToString());
        PlayerPrefs.SetString("resolution", PauseMenus.resolution.ToString());
        PlayerPrefs.SetString("quality", PauseMenus.quality.ToString());
        PlayerPrefs.SetString("audioSettings", PauseMenus.audioSettings.ToString());

        //PlayerPrefs.Save();
    }
}
