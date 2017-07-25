using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BasicMenusScript : MonoBehaviour {
    [SerializeField]
    List<GameObject> button = new List<GameObject>();
    UnityEngine.EventSystems.EventSystem myEvent;
    GameObject lastSelected;
    [SerializeField]
    AudioClip hover;
    [SerializeField]
    AudioClip click;
    [SerializeField]
    AudioSource au;
    public static bool canSelect = true; //determiens whether or not the players can select the menu
    bool isRunning; //determiens wheter or not the credits courotine is running

    bool isLerping = false; //this is used to determine whether or not the coroutine is running

    void Update()
    {
        //if (!myEvent.currentSelectedGameObject)
        //     myEvent.SetSelectedGameObject(lastSelected);
        if (Input.anyKeyDown && isRunning)
        {
            //isRunning = false;
            Animator anim = GameObject.Find("Canvas/Menus/Credits/List").GetComponent<Animator>();
            anim.speed = 10;
            GameObject go = GameObject.Find("Canvas/Menus/Credits/SkipText");
            go.SetActive(false);
        }

    }


    // Use this for initialization
    void Start ()
    {
        SaveLoadPrefs.Load();
        canSelect = true;
        au.volume = PauseMenus.SFXvolume;
        //Time.timeScale = PauseMenus.gameSpeed;

        myEvent = GameObject.Find("Canvas/EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        //finding all butons within the menu
        Transform menu = gameObject.transform;
		foreach (Transform child in menu)
        {
            button.Add(child.gameObject);
        }
        myEvent.SetSelectedGameObject(button[0]);
        GameObject.Find("Canvas/Menus/GameTitle").GetComponent<Animator>().Play("Intro",0,0);
        GameObject.Find("Canvas/Menus").GetComponent<Animator>().Play("Introduction", 0, 0);
        Animator anim = GameObject.Find("Canvas/Background").GetComponent<Animator>();
        anim.speed = 1;
        anim.Play("FromBlack", 0, 0);
    }
	
	public void SelectingNew()
    {
        if (isLerping || !canSelect)
            return;
        au.pitch = 1f;
        isLerping = true;
        au.PlayOneShot(hover, PauseMenus.SFXvolume);
 
        Color c = button[0].GetComponent<Text>().color;
        c.a = 0;
        Transform menu = gameObject.transform;
        foreach (Transform child in menu)
        {
            child.gameObject.GetComponent<Text>().color = c;
            child.gameObject.GetComponent<Text>().raycastTarget = false;
            child.gameObject.GetComponent<Animator>().enabled = false;
            child.gameObject.GetComponent<Transform>().localScale = new Vector3 (1,1,1);
        }
        if (!myEvent.currentSelectedGameObject)
        return;
        c.a = 1f;
        myEvent.currentSelectedGameObject.GetComponent<Text>().fontSize = 33;
        myEvent.currentSelectedGameObject.GetComponent<Text>().color = c;
        myEvent.currentSelectedGameObject.GetComponent<Animator>().enabled = true;
        c.a = .8f;
        if (myEvent.currentSelectedGameObject == button[0])
        {
            lastSelected = button[0];
            lastSelected.GetComponent<Text>().raycastTarget = true;
            button[button.Count-1].GetComponent<Text>().fontSize = 30;
            button[button.Count-1].GetComponent<Text>().color = c;
            button[1].GetComponent<Text>().fontSize = 30;
            button[1].GetComponent<Text>().color = c;
            StartCoroutine(Lerp(transform.rotation,Quaternion.Euler(0, 0, 82), button.Count - 1,1));
            //StartCoroutine(FadeColors(button[0].GetComponent<Text>().color, button[0].GetComponent<Text>(), button[1].GetComponent<Text>(), button[2].GetComponent<Text>(), button[3].GetComponent<Text>()));
            //transform.rotation = Quaternion.Euler(0, 0, 82);
        }
        else if (myEvent.currentSelectedGameObject == button[5])
        {
            lastSelected = button[5];
            lastSelected.GetComponent<Text>().raycastTarget = true;
            button[4].GetComponent<Text>().fontSize = 30;
            button[4].GetComponent<Text>().color = c;
            button[0].GetComponent<Text>().fontSize = 30;
            button[0].GetComponent<Text>().color = c;
            StartCoroutine(Lerp(transform.rotation, Quaternion.Euler(0, 0, 150), 0,4));
            //transform.rotation = Quaternion.Euler(0, 0, 150);
        }
        else if (myEvent.currentSelectedGameObject == button[1])
        {
            lastSelected = button[1];
            lastSelected.GetComponent<Text>().raycastTarget = true;
            button[2].GetComponent<Text>().fontSize = 30;
            button[2].GetComponent<Text>().color = c;
            button[0].GetComponent<Text>().fontSize = 30;
            button[0].GetComponent<Text>().color = c;
            button[0].GetComponent<Text>().raycastTarget = true;
            button[2].GetComponent<Text>().raycastTarget = true;
            StartCoroutine(Lerp(transform.rotation, Quaternion.Euler(0, 0, 24), 2, 0));
            // transform.rotation = Quaternion.Euler(0, 0, 24);
        }
        else if (myEvent.currentSelectedGameObject == button[2])
        {
            lastSelected = button[2];
            lastSelected.GetComponent<Text>().raycastTarget = true;
            button[3].GetComponent<Text>().fontSize = 30;
            button[3].GetComponent<Text>().color = c;
            button[1].GetComponent<Text>().fontSize = 30;
            button[1].GetComponent<Text>().color = c;
            button[3].GetComponent<Text>().raycastTarget = true;
            button[1].GetComponent<Text>().raycastTarget = true;
            StartCoroutine(Lerp(transform.rotation, Quaternion.Euler(0, 0, -32), 3, 1));
            // transform.rotation = Quaternion.Euler(0, 0, -32);
        }
        else if (myEvent.currentSelectedGameObject == button[3])
        {
            lastSelected = button[3];
            lastSelected.GetComponent<Text>().raycastTarget = true;
            button[4].GetComponent<Text>().fontSize = 30;
            button[4].GetComponent<Text>().color = c;
            button[2].GetComponent<Text>().fontSize = 30;
            button[2].GetComponent<Text>().color = c;
            button[4].GetComponent<Text>().raycastTarget = true;
            button[2].GetComponent<Text>().raycastTarget = true;
            StartCoroutine(Lerp(transform.rotation, Quaternion.Euler(0, 0, 265),4, 2));
            //transform.rotation = Quaternion.Euler(0, 0, 265);
        }
        else if (myEvent.currentSelectedGameObject == button[4])
        {
            lastSelected = button[4];
            lastSelected.GetComponent<Text>().raycastTarget = true;
            button[5].GetComponent<Text>().fontSize = 30;
            button[5].GetComponent<Text>().color = c;
            button[3].GetComponent<Text>().fontSize = 30;
            button[3].GetComponent<Text>().color = c;
            StartCoroutine(Lerp(transform.rotation, Quaternion.Euler(0, 0, 200), 3, 5));
            // transform.rotation = Quaternion.Euler(0, 0, 200);
        }
        foreach (Transform child in menu)
        {
            child.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    IEnumerator Lerp(Quaternion a, Quaternion b, int one, int two)
     {
        Transform menu = gameObject.transform;
        float lerpTime = 0;
        button[one].GetComponent<Text>().raycastTarget = false;
        button[two].GetComponent<Text>().raycastTarget = false;
        while (lerpTime < .2f)
        {
            transform.rotation = Quaternion.Lerp(a, b, (lerpTime/.2f));
            lerpTime += Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(.02f);
        }
        foreach (Transform child in menu)
        {
            child.gameObject.GetComponent<Button>().enabled = true;
        }
        button[one].GetComponent<Text>().raycastTarget = true;
        button[two].GetComponent<Text>().raycastTarget = true;
        transform.rotation = b;
        isLerping = false;
     }

    public void GameStart()
    {
        au.pitch = 1f;
        canSelect = false;
        Color c = button[0].GetComponent<Text>().color;
        c.a = 0;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
        MusicScript music = GameObject.Find("Game Manager/Music").GetComponent<MusicScript>();
        music.StopAllCoroutines();
        music.StartCoroutine(music.MusicOff());
        GameObject.Find("Canvas/Menus/Basic/Campaign").GetComponent<Animator>().enabled = false;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.GetComponent<Text>().color = c;
            child.gameObject.GetComponent<Text>().raycastTarget = false;
            child.gameObject.transform.localScale = new Vector3(1,1,1);
        }
        c.a = .05f;
        GameObject.Find("Canvas/Menus/Basic/Campaign").GetComponent<Text>().color = c;
        GameObject.Find("Canvas/Menus/Basic/Quit").GetComponent<Text>().color = c;
        GameObject.Find("Canvas/Menus/Basic/Arcade").GetComponent<Text>().color = c;
        StartCoroutine(MyCor());
    }
    public IEnumerator MyCor()
    {
        Animator title = GameObject.Find("Canvas/Menus/GameTitle").GetComponent<Animator>();
        title.Play("Crumble",0,0);
        yield return new WaitForSecondsRealtime(2.5f);
        SceneManager.LoadScene("Level01");
    }
    //arcade mode
    public void Arcade()
    {
        au.pitch = 1f;
        canSelect = false;
        Color c = button[0].GetComponent<Text>().color;
        c.a = 0;
        au.PlayOneShot(click, PauseMenus.SFXvolume);
        MusicScript music = GameObject.Find("Game Manager/Music").GetComponent<MusicScript>();
        music.StopAllCoroutines();
        music.StartCoroutine(music.MusicOff());
        GameObject.Find("Canvas/Menus/Basic/Campaign").GetComponent<Animator>().enabled = false;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.GetComponent<Text>().color = c;
            child.gameObject.GetComponent<Text>().raycastTarget = false;
            child.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        c.a = .05f;
        GameObject.Find("Canvas/Menus/Basic/Campaign").GetComponent<Text>().color = c;
        GameObject.Find("Canvas/Menus/Basic/Leaderboards").GetComponent<Text>().color = c;
        GameObject.Find("Canvas/Menus/Basic/Arcade").GetComponent<Text>().color = c;
        StartCoroutine(StartArcade());
    }
    public IEnumerator StartArcade()
    {
        Animator title = GameObject.Find("Canvas/Menus/GameTitle").GetComponent<Animator>();
        title.Play("Break", 0, 0);
        yield return new WaitForSecondsRealtime(.5f);
        SceneManager.LoadScene("Level01");
    }

    public void Credits()
    {
        au.pitch = 1f;
        Animator anim1 = GameObject.Find("Canvas/Menus/GameTitle").GetComponent<Animator>();
        anim1.Play("CreditChange");
        
            au.PlayOneShot(click, PauseMenus.SFXvolume);
            Color c = button[0].GetComponent<Text>().color;
            c.a = 0;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.GetComponent<Text>().color = c;
                child.gameObject.GetComponent<Text>().raycastTarget = false;
            }
            StartCoroutine(CreditCoroutine());

    }

    public IEnumerator CreditCoroutine()
    {
        canSelect = false;
        isRunning = true;
        Animator anim = GameObject.Find("Canvas/Background").GetComponent<Animator>();
        anim.speed = 1;
        anim.Play("TurnBlue", 0, 0);
        anim = GameObject.Find("Canvas/Menus/Credits/List").GetComponent<Animator>();
        anim.speed = 1;
        anim.Play("Roll", 0, 0);
        while (anim.GetCurrentAnimatorStateInfo(0).length == 1)
        {
            yield return null;
        }
        GameObject go = GameObject.Find("Canvas/Menus/Credits/SkipText");
        go.SetActive(true);
        Color c = button[0].GetComponent<Text>().color;
        c.a = 0f;
        Animator anim1 = GameObject.Find("Canvas/Menus/GameTitle").GetComponent<Animator>();
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        isRunning = false;
        go = GameObject.Find("Canvas/Menus/Credits/SkipText");
        go.SetActive(false);
        anim.speed = 1;
        anim1.Play("ChangeTitle", 0, 0);
        anim = GameObject.Find("Canvas/Background").GetComponent<Animator>();
        anim.speed = 1;
        anim.Play("FromBlue", 0, 0);
        yield return new WaitForSeconds(1f);
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.GetComponent<Text>().color = c;
            child.gameObject.GetComponent<Text>().raycastTarget = false;
        }
        Transform temp = transform.Find("Credits");
        c.a = 1f;
        temp.gameObject.GetComponent<Text>().color = c;
        temp.gameObject.GetComponent<Text>().raycastTarget = true;
        c.a = .05f;
        temp = transform.Find("Quit");
        temp.gameObject.GetComponent<Text>().raycastTarget = true;
        temp.gameObject.GetComponent<Text>().color = c;
        temp = transform.Find("Options");
        temp.gameObject.GetComponent<Text>().raycastTarget = true;
        temp.gameObject.GetComponent<Text>().color = c;
        canSelect = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
