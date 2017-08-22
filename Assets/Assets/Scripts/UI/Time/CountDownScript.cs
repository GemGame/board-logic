using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour {

    float countDownTimer = 60;
    public float CountDowTimer
    {
        get { return countDownTimer; }
    }
    public float Turns
    {
        get { return manageScore.PlayerTurns; }
    }
    float miliSeconds;
    [Range (.02f,10f)]
    public float countDownRate;
    [SerializeField]
    Text myTitle; //the title above the text. For example, in time attack,"Time" will be written above the countdown timer.
    [SerializeField]
    Text AddTimeText;
    Text myText;
    Outline myOutline;

    [Header ("Outine Color")]
    [SerializeField]
    Color start;
    [SerializeField]
    Color end;
    [Header("Text Color")]
    [SerializeField]
    Color _start;
    [SerializeField]
    Color _end;
    [SerializeField]
    AudioSource sounds;
    [SerializeField]
    AudioClip tick;
    [SerializeField]
    GameObject results;
    [SerializeField]
    GameObject menu;
    [SerializeField]
    ManageScore manageScore;


    private void Start()
    {
        manageScore.PlayerTurns = manageScore.startingTurns;
        myText = gameObject.GetComponent<Text>();
        myOutline = gameObject.GetComponent<Outline>();
        if (manageScore.gameRules == ManageScore.GameRules.TimeAttack)
        {
            myTitle.text = "Time";
            countDownTimer = manageScore.countDownTime;
            StartCoroutine(CountDown());
        }
        else
        {
            myTitle.text = "Turns";
            myText.text = manageScore.PlayerTurns.ToString();
        }
    }

    public void AddCountDown(float addTime)
    {
        countDownTimer = addTime;
    } 
    //counting down until the game is over
    public IEnumerator CountDown()
    {
        float placeholder = countDownTimer;
        while (countDownTimer > 0 || miliSeconds > 0)
        {
            yield return new WaitForSeconds(countDownRate);
            if (miliSeconds > 0)
                miliSeconds -= 1;
            else if (miliSeconds <= 0 && countDownTimer > 0)
            {
                miliSeconds = 10f;
                countDownTimer -= 1;
                if (countDownTimer > 10)
                    gameObject.GetComponent<Animator>().Play("Subtract", 0, 0);
                else
                    gameObject.GetComponent<Animator>().Play("DangerouslyLow", 0, 0);
                if (countDownTimer <= 10)
                    sounds.PlayOneShot(tick, PauseMenus.SFXvolume);
            }
            //myText.text = string.Format("{0}.{1}", countDownTimer, (int)miliSeconds);
            myText.text = countDownTimer.ToString();
            Color lerpedOutline = Color.Lerp(end, start, countDownTimer/placeholder);
            Color lerpedFont = Color.Lerp(_end, _start, countDownTimer/placeholder);
            myOutline.effectColor = lerpedOutline;
            myText.color = lerpedFont;
        }
        //ending the game and showing the results
        PauseMenus.gamePaused = true;
        menu.SetActive(false);
        Transform time = GameObject.Find("Time").transform;
        foreach (Transform text in time)
        {
            text.GetComponent<Text>().text = " ";
        }
        yield return new WaitForSecondsRealtime(2f);
       myText.gameObject.SetActive(false);
       myTitle.gameObject.SetActive(false);
       AddTimeText.gameObject.SetActive(false);
    }


    //dealing with turn based game rules
    public void SetTurns(int doMath) //pass the values through for which you want to add or subtract
    {
        if (manageScore.gameRules != ManageScore.GameRules.TurnBase)
            return;
        manageScore.PlayerTurns += doMath;
        if (manageScore.PlayerTurns <= 0)
            manageScore.PlayerTurns = 0;
        myText.text = manageScore.PlayerTurns.ToString();
        //animate
        if (manageScore.PlayerTurns >= 3)
            gameObject.GetComponent<Animator>().Play("Subtract", 0, 0);
        else
            gameObject.GetComponent<Animator>().Play("DangerouslyLow", 0, 0);
        if (manageScore.PlayerTurns <= 0)
            StartCoroutine(Wait());


    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2f);
        myText.gameObject.SetActive(false);
        myTitle.gameObject.SetActive(false);
        AddTimeText.gameObject.SetActive(false);
    }

    public void AddTime(int secs)
    {
        if (manageScore.gameRules != ManageScore.GameRules.TimeAttack)
            return;
        countDownTimer += secs;
        AddTimeText.text = "+" + secs + " secs";
        AddTimeText.gameObject.GetComponent<Animator>().Play("Add", 0, 0);

    }
}
