using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PauseMenus : MonoBehaviour {

    [SerializeField]
    GameObject pause;
    [SerializeField]
    GameObject pauseButton;
    [SerializeField]
    GameObject pauseMenus;
    [SerializeField]
    [Tooltip("The animator from the gameobject, 'Pause > Menus'")]
    Animator menusAnim;
    bool isPlaying; //this bool is used to determine whether or not an animation is in play.
    [SerializeField]
    AudioClip hover;
    [SerializeField]
    AudioClip select;
    AudioSource _audio;
    [SerializeField]
    [Tooltip("Canvas > Menus > OptionMenus > DiffText")]
    Text diffText;
    [Header("The slider for the child game objects of BGMVolume, SFXvolume, and Brightness")]
    [SerializeField]
    Slider BGMSlider;
    [SerializeField]
    Slider SFXSlider;
    [SerializeField]
    Slider brightnessSlider;

    bool mute = false;//determiens whether or not all music/sounds are muted
    public static bool gamePaused;

    //this enum determins which menu the player is currently on
    public enum CurMenu { Pause, Options, Audio, Video, Controls, Reset, Quit }
    public static CurMenu curMenu;
    public enum AudioSettings { Stereo, Mono }
    public static AudioSettings audioSettings;
    public enum Difficulty { Easy, Normal, Hard }
    public static Difficulty difficulty;
    public enum Quality { Poor, Good, High }
    public static Quality quality;
    public enum GameSpeed { Slowest, Slow, Normal, Fast, Fastest }
    public static GameSpeed gameSpeed;
    public enum Resolution { Small, Medium, Fullscreen }
    [HideInInspector]
    public static Resolution resolution;
    public static float scaleTime = 1f;

    public static float BGMvolume = .3f;
    public static float SFXvolume = .8f;
    public static float brightness;

    void Start()
    {
        curMenu = CurMenu.Pause;
        //just to avoid any null references for the static variable, we're going to find the audio source
        _audio = gameObject.GetComponent<AudioSource>();
        AudioSource music = GameObject.Find("Game Manager/Music").GetComponent<AudioSource>();
        MusicScript.auSource = music;

        //setting defaults
        difficulty = Difficulty.Normal;
        gameSpeed = GameSpeed.Slow;
        resolution = Resolution.Fullscreen;
        scaleTime = 1f;
        SFXvolume = .8f;
        BGMvolume = .3f;
        //loading all player prefs
        SaveLoadPrefs.Load();
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && (pauseMenus.activeSelf || !pause.activeSelf) && curMenu == CurMenu.Pause && !isPlaying)
        {//pausing the game
            Pause();
        }
    }

    //Pauses the game or resumes the game if it's already paused
    public void Pause()
    {
        {
            if (!pause.activeSelf)
            {
                pause.SetActive(true);
                pauseButton.SetActive(false);
                Time.timeScale = 0;
            }
            else
            {
                pause.SetActive(false);
                pauseButton.SetActive(true);
                Time.timeScale = scaleTime;
            }
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
        }
    }

    //sends the player to the video options
    public void Video()
    {
        if (!isPlaying)
        {//checking if an animation is playing before accessing another menu
            isPlaying = true;
            _audio.pitch = 1f;
            _audio.PlayOneShot(select, SFXvolume);
            if (PlayerPrefs.HasKey("resolution"))
                PauseMenus.resolution = (PauseMenus.Resolution)System.Enum.Parse(typeof(PauseMenus.Resolution), PlayerPrefs.GetString("resolution"));
            curMenu = CurMenu.Video;
            Text myText;
            Slider myslider;
            //adjusting the texts/strings to match their current settings
            if (PlayerPrefs.HasKey("brightnessSlider"))
            {
                myslider = GameObject.Find("Canvas/Pause/Menus/VideoMenus/Brightness/Slider").GetComponent<Slider>();
                myslider.value = PlayerPrefs.GetInt("brightnessSlider");
                myText = GameObject.Find("Canvas/Pause/Menus/VideoMenus/BrightnessValue").GetComponent<Text>();
                myText.text = PlayerPrefs.GetInt("brightnessSlider").ToString();
            }
            if (PlayerPrefs.HasKey("quality"))
            {
                PauseMenus.quality = (PauseMenus.Quality)System.Enum.Parse(typeof(PauseMenus.Quality), PlayerPrefs.GetString("quality"));
                myText = GameObject.Find("Canvas/Pause/Menus/VideoMenus/QualityValue").GetComponent<Text>();
                myText.text = quality.ToString();
            }
            myText = GameObject.Find("Canvas/Pause/Menus/VideoMenus/ResolutionValue").GetComponent<Text>();
            if (PlayerPrefs.HasKey("resolution"))
            {
                PauseMenus.resolution = (PauseMenus.Resolution)System.Enum.Parse(typeof(PauseMenus.Resolution), PlayerPrefs.GetString("resolution"));
                switch (resolution)
                {
                    default:
                        myText.text = "Full Screen";
                        break;
                    case Resolution.Small:
                        myText.text = "600x400";
                        break;
                    case Resolution.Medium:
                        myText.text = "800x600";
                        break;
                }
            }

            menusAnim.Play("CloseOptions");
            StartCoroutine(WaitTime(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenVideo"));
        }
    }

    //adjusting the resolution
    public void ScreenResolution()
    {
        if (!isPlaying)
        {
            Text myText = GameObject.Find("Canvas/Pause/Menus/VideoMenus/ResolutionValue").GetComponent<Text>();
            switch (resolution)
            {
                default:                  
                    Screen.SetResolution(600, 400, false);
                    resolution = Resolution.Small;
                    myText.text = "600x400";
                    break;
                case Resolution.Small:
                    Screen.SetResolution(800, 600, false);
                    resolution = Resolution.Medium;
                    myText.text = "800x600";
                    break;
                case Resolution.Medium:
                    Screen.SetResolution(Screen.width, Screen.height, true);
                    resolution = Resolution.Fullscreen;
                    myText.text = "Full Screen";
                    break;
            }
            _audio.pitch = 1f;
            _audio.PlayOneShot(select, SFXvolume);
        }
    }

    public void Mute()
    {
        AudioSource sounds = GameObject.Find("Game Manager/Sound").GetComponent<AudioSource>();
        AudioSource music = GameObject.Find("Game Manager/Music").GetComponent<AudioSource>();
        Image image = GameObject.Find("Canvas/HUDButtons/Mute").GetComponent<Image>();

        if (mute)
        {
            mute = false;
            gameObject.GetComponent<AudioSource>().mute = mute;
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
            image.sprite = Resources.Load<Sprite>("Art/HUD/Sounds/Unmute");
        }
        else
        {
            mute = true;
            gameObject.GetComponent<AudioSource>().mute = true;
            image.sprite = Resources.Load<Sprite>("Art/HUD/Sounds/Mute");
        }
        sounds.mute = mute;
        music.mute = mute;
    }

    public void ResetLevel()
    {
        if (!isPlaying)
        {//checking if an animation is playing before accessing another menu
         //reset score, reset gems, reset board
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
        }
    }

    //sends the player to the audio options
    public void Audio()
    {
        if (!isPlaying)
        {//checking if an animation is playing before accessing another menu
            isPlaying = true;
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
            curMenu = CurMenu.Audio;
            //setting all the text to their current values
            Text myText = GameObject.Find("Canvas/Pause/Menus/AudioMenus/BGMValue").GetComponent<Text>();
            myText.text = (BGMvolume * 10).ToString();
            myText = GameObject.Find("Canvas/Pause/Menus/AudioMenus/SFXValue").GetComponent<Text>();
            myText.text = (SFXvolume * 10).ToString();
            if (PlayerPrefs.HasKey("audioSettings"))
            {
                myText = GameObject.Find("Canvas/Pause/Menus/AudioMenus/Audio").GetComponent<Text>();
                myText.text = audioSettings.ToString();
            }
            Slider myslider = GameObject.Find("Canvas/Pause/Menus/AudioMenus/SFXVolume/Slider").GetComponent<Slider>();
            myslider.value = (int)(SFXvolume*10);
            myslider = GameObject.Find("Canvas/Pause/Menus/AudioMenus/BGMVolume/Slider").GetComponent<Slider>();
            myslider.value = (int)(BGMvolume*10);

            menusAnim.Play("CloseOptions");
            StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenAudio"));
        }
    }
    //sends the player to the Controls
    public void Controls()
    {
        curMenu = CurMenu.Controls;
        menusAnim.Play("CloseOptions");
        StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "Controls"));
        _audio.pitch = 1.2f;
        _audio.PlayOneShot(select, SFXvolume);
    }

    //sends the player to the audio options
    public void Options()
    {
        if (!isPlaying)
        {//checking if an animation is playing before accessing another menu
            isPlaying = true;
            curMenu = CurMenu.Options;
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
            menusAnim.Play("ClosePauseMenus");
            StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenOptions"));

            //Here we are just making sure the enums match their texts/strings
            Text temp;
            if (PlayerPrefs.HasKey("gameSpeed"))
            {
                temp = GameObject.Find("Canvas/Pause/Menus/OptionMenus/GameSpeedValue").GetComponent<Text>();
                temp.text = PlayerPrefs.GetString("gameSpeed").ToString();
            }
            if (PlayerPrefs.HasKey("difficulty"))
            {
                temp = GameObject.Find("Canvas/Pause/Menus/OptionMenus/DiffText").GetComponent<Text>();
                PauseMenus.difficulty = (PauseMenus.Difficulty)System.Enum.Parse(typeof(PauseMenus.Difficulty), PlayerPrefs.GetString("difficulty"));
                temp.text = difficulty.ToString();
            }
        }
    }
    
    //double checks with player before they quit (or reset the level)
    //if the bool, quit (in the parameters), is set to true, it returns to menu, else if it is false it will reset the level
    public void Quit(bool quit)
    {
        if (!isPlaying)
        {//checking if an animation is playing before accessing another menu
            isPlaying = true;
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
            if (quit)//returns to main menu
                curMenu = CurMenu.Quit;
             else//resets the level
                curMenu = CurMenu.Reset;
            menusAnim.Play("ClosePauseMenus");
            StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenQuit"));
        }
    }

    //returns player to main lobby/main menu
    //this function also resets the level. It all depends on the enum CurMenu which gets changed in the function "Quit(bool)"
    public void MainMenu()
    {
        if (!isPlaying)
        {//checking if an animation is playing before accessing another menu
         //load main menu here
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
            if (curMenu == CurMenu.Quit)
            {
                gamePaused = false;
                Animator effect = GameObject.Find("Canvas/ScreenEffect").GetComponent<Animator>();
                effect.Play("TransitionOut");
                StartCoroutine(FadeOut(1f));
            }
            else if (curMenu == CurMenu.Reset)
            {
                ResetLevel();
                print("The game should reset current level"); //application.quit should be applied here
            }
        }
    }

    //change difficulty
    public void Diff()
    {
        if (!isPlaying)
        {//checking if an animation is playing before accessing another menu
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
            switch (difficulty)
            {
                default:
                    difficulty = Difficulty.Normal;
                    diffText.text = "Normal";
                break;
                case Difficulty.Normal:
                    difficulty = Difficulty.Hard;
                    diffText.text = "Hard";
                    break;
                case Difficulty.Hard:
                    difficulty = Difficulty.Easy;
                    diffText.text = "Easy";
                    break;
            }
        }
    }

    //the audio settings to change from either mono to stereo
    public void AuSettings()
    {
        if (!isPlaying)
        {//checking if an animation is playing
            if (!isPlaying)
            {
                Text audioText = GameObject.Find("Canvas/Pause/Menus/AudioMenus/Audio").GetComponent<Text>();
                switch (audioSettings)
                {//determining which audio setting is currently being set
                    default:
                        audioSettings = AudioSettings.Stereo;
                        audioText.text = "Stereo";
                        gameObject.GetComponent<AudioHighPassFilter>().enabled = false;
                        AudioHighPassFilter sound = GameObject.Find("Game Manager/Sound").GetComponent<AudioHighPassFilter>();
                        sound.enabled = false;
                        sound = GameObject.Find("Game Manager/Music").GetComponent<AudioHighPassFilter>();
                        sound.enabled = false;
                        break;
                    case AudioSettings.Stereo:
                        audioSettings = AudioSettings.Mono;
                        audioText.text = "Mono";
                        gameObject.GetComponent<AudioHighPassFilter>().enabled = true;
                        sound = GameObject.Find("Game Manager/Sound").GetComponent<AudioHighPassFilter>();
                        sound.enabled = true;
                        sound = GameObject.Find("Game Manager/Music").GetComponent<AudioHighPassFilter>();
                        sound.enabled = true;
                        break;
                }
                _audio.pitch = 1.2f;
                _audio.PlayOneShot(select, SFXvolume);
            }
        }
    }

    //the audio settings to change from either mono to stereo
    public void SetQuality()
    {
        if (!isPlaying)
        {//checking if an animation is playing
                Text qualityText = GameObject.Find("Canvas/Pause/Menus/VideoMenus/QualityValue").GetComponent<Text>();
                switch (quality)
                {
                    default:
                        quality = Quality.Good;
                        qualityText.text = "Good";
                        QualitySettings.SetQualityLevel(1);
                        break;
                    case Quality.Good:
                        quality = Quality.High;
                        qualityText.text = "High";
                        QualitySettings.SetQualityLevel(2);
                        break;
                    case Quality.High:
                        quality = Quality.Poor;
                        qualityText.text = "Poor";
                        QualitySettings.SetQualityLevel(0);
                        break;
                }
                _audio.pitch = 1.2f;
                _audio.PlayOneShot(select, SFXvolume);
        }
    }

    //the audio settings to change from either mono to stereo
    public void SetGameSpeed()
    {
        if (!isPlaying)
        {//checking if an animation is playing
                Text qualityText = GameObject.Find("Canvas/Pause/Menus/OptionMenus/GameSpeedValue").GetComponent<Text>();
                switch (gameSpeed)
                {
                 default:
                    gameSpeed = GameSpeed.Normal;
                    qualityText.text = "Normal";
                    scaleTime = 1.2f;
                    break;
                 case GameSpeed.Normal:
                    gameSpeed = GameSpeed.Fast;
                    qualityText.text = "Fast";
                    scaleTime = 1.4f;
                    break;
                 case GameSpeed.Fast:
                    gameSpeed = GameSpeed.Fastest;
                    qualityText.text = "Fastest";
                    scaleTime = 1.6f;
                    break;
                case GameSpeed.Fastest:
                    gameSpeed = GameSpeed.Slowest;
                    qualityText.text = "Slowest";
                    scaleTime = .8f;
                    break;
                case GameSpeed.Slowest:
                    gameSpeed = GameSpeed.Slow;
                    qualityText.text = "Slow";
                    scaleTime = 1f;
                    break;
            }
                _audio.pitch = 1.2f;
                _audio.PlayOneShot(select, SFXvolume);
        }
    }
    //adjusting the BGM volume
    public void BGM()
    {
        BGMvolume = BGMSlider.value/10;
        MusicScript.auSource.volume = BGMvolume;
        //changing the text to match the volume
        Text myText = GameObject.Find("Canvas/Pause/Menus/AudioMenus/BGMValue").GetComponent<Text>();
        myText.text = BGMSlider.value.ToString();
        _audio.pitch = 1.2f;
        _audio.PlayOneShot(select, SFXvolume);
    }

    //adjusting the BGM volume
    public void Brightness()
    {
        brightness = 255/(brightnessSlider.value * 255);
        //changing the text to match the value of brightness
        Text myText = GameObject.Find("Canvas/Pause/Menus/VideoMenus/BrightnessValue").GetComponent<Text>();
        myText.text = brightnessSlider.value.ToString();
        //changing games' brightness to match the slider's value
        Image myImage = GameObject.Find("Canvas/Brightness").GetComponent<Image>();
        Color c = Color.black;
        c.a = brightness;
        myImage.color = c;
        _audio.pitch = 1.2f;
        _audio.PlayOneShot(select, SFXvolume);
    }

    //adjusting the SFX volume
    public void SFX()
    {
        SFXvolume = SFXSlider.value / 10;
        //changing the text to match the volume
        Text myText = GameObject.Find("Canvas/Pause/Menus/AudioMenus/SFXValue").GetComponent<Text>();
        myText.text = SFXSlider.value.ToString();
        _audio.pitch = 1.2f;
        _audio.PlayOneShot(select, SFXvolume);
    }

    //returns the player from the current menu to the previous menu
    public void Return()
    {
        if (!isPlaying)
        {//checking if an animation is playing before accessing another menu
            isPlaying = true;
            _audio.pitch = 1.2f;
            _audio.PlayOneShot(select, SFXvolume);
            switch (curMenu)
            {
                default:
                    break;
                case CurMenu.Options:
                    menusAnim.Play("CloseOptions");
                    curMenu = CurMenu.Pause;
                    StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenPauseMenus"));
                    break;
                case CurMenu.Audio:
                    menusAnim.Play("CloseAudio");
                    curMenu = CurMenu.Options;
                    StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenOptions"));
                    break;
                case CurMenu.Video:
                    PlayerPrefs.SetInt("brightnessSlider", (int)GameObject.Find("Canvas/Pause/Menus/VideoMenus/Brightness/Slider").GetComponent<Slider>().value);
                    menusAnim.Play("CloseVideo");
                    curMenu = CurMenu.Options;
                    StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenOptions"));
                    break;
                case CurMenu.Quit:
                    menusAnim.Play("CloseQuit");
                    curMenu = CurMenu.Pause;
                    StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenPauseMenus"));
                    break;
                case CurMenu.Controls:
                    menusAnim.Play("CloseControls");
                    curMenu = CurMenu.Options;
                    StartCoroutine(MyCoroutine(menusAnim.GetCurrentAnimatorStateInfo(0).length, "OpenOptions"));
                    break;
                case CurMenu.Reset:
                    goto case CurMenu.Quit;
            }
        }
        SaveLoadPrefs.Save();
    }
    //this changes the font for the current selected menu item and returns the font size of all other menu options
    public void Highlight()
    {
        if (isPlaying)
        return;
        switch (curMenu)
        {
            default:
                GameObject menus = GameObject.Find("Canvas/Pause/Menus/PauseMenus");
                ChangeFont(menus);
                break;
            case CurMenu.Options:
                menus = GameObject.Find("Canvas/Pause/Menus/OptionMenus");
                ChangeFont(menus);
                break;
            case CurMenu.Audio:
                menus = GameObject.Find("Canvas/Pause/Menus/AudioMenus");
                ChangeFont(menus);
                break;
            case CurMenu.Video:
                menus = GameObject.Find("Canvas/Pause/Menus/VideoMenus");
                ChangeFont(menus);
                break;
            case CurMenu.Quit:
                menus = GameObject.Find("Canvas/Pause/Menus/QuitGameMenus");
                ChangeFont(menus);
                break;
            case CurMenu.Reset:
                goto case CurMenu.Quit;
        }
        _audio.pitch = 1f;
        _audio.PlayOneShot(hover, SFXvolume);
    }

    void ChangeFont(GameObject menus)
    {
        //finding the event system and assigning it to the temp variable, "menus."
        UnityEngine.EventSystems.EventSystem myEvent = GameObject.Find("Canvas/EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        foreach (Transform menu in menus.transform)
        {
            menu.gameObject.GetComponent<Text>().fontSize = 30;
            menu.gameObject.GetComponent<Outline>().enabled = false;
            menu.gameObject.transform.localScale = new Vector3 (1,1,1);
        }
        myEvent.currentSelectedGameObject.GetComponent<Text>().fontSize = 32;
        myEvent.currentSelectedGameObject.GetComponent<Outline>().enabled = true;
    }

    private IEnumerator MyCoroutine(float wait, string anim)
    {
        yield return StartCoroutine(WaitTime(wait, anim));
    }

    //these two coroutines are for the fade out screen
    private IEnumerator FadeOut(float wait)
    {
        yield return StartCoroutine(QuitTime(wait));
    }

    IEnumerator QuitTime(float wait)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + wait)
        {
            yield return null;
        }
        SceneManager.LoadScene("MainLobby");
    }

    IEnumerator WaitTime(float wait, string anim)
    {
        //finding the event system and assigning it to this temp variable
        UnityEngine.EventSystems.EventSystem myEvent = GameObject.Find("Canvas/EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + wait)
        {
            yield return null;
        }
        //playing the next animations by which the string is passed through the parameters of this function
        menusAnim.Play(anim);
        //finding the first button in the menu and setting that as the current selected button
        switch (curMenu)
        {
            default:
                myEvent.SetSelectedGameObject(GameObject.Find("Canvas/Pause/Menus/PauseMenus/Resume"));
                break;
            case CurMenu.Options:
                myEvent.SetSelectedGameObject(GameObject.Find("Canvas/Pause/Menus/OptionMenus/Difficulty"));
                break;
            case CurMenu.Audio:
                myEvent.SetSelectedGameObject(GameObject.Find("Canvas/Pause/Menus/AudioMenus/BGMVolume"));
                break;
            case CurMenu.Video:
                myEvent.SetSelectedGameObject(GameObject.Find("Canvas/Pause/Menus/VideoMenus/Brightness"));
                break;
            case CurMenu.Quit:
                myEvent.SetSelectedGameObject(GameObject.Find("Canvas/Pause/Menus/QuitGameMenus/Cancel"));
                if (curMenu == CurMenu.Quit)
                {//just setting the text and changing it depending upon whether or not the player decided to quit or reset the level
                    Text myText = GameObject.Find("Canvas/Pause/Menus/QuitGameMenus/Text").GetComponent<Text>();
                    myText.text = "Quit Game?";
                }
                else
                {
                    Text myText = GameObject.Find("Canvas/Pause/Menus/QuitGameMenus/Text").GetComponent<Text>();
                    myText.text = "Reset Level?";
                }
                break;
            case CurMenu.Reset:
                goto case CurMenu.Quit;
        }
        start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + menusAnim.GetCurrentAnimatorStateInfo(0).length)
        {
            yield return null;
        }
        isPlaying = false;
    }
}
