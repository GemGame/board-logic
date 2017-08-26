using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddingScore : MonoBehaviour
{
    //int myScore;
    int add;
    [SerializeField]
    Text myScore;
    IEnumerator _score;
    [SerializeField]
    AudioSource au;
    [SerializeField]
    AudioClip taDa;
    [SerializeField]
    AudioClip collectSound;
    [SerializeField]
    Animator scoreBar;
    [SerializeField]
    Animator scoreBarText;
    [SerializeField]
    ManageScore manageScoreScript;
    [SerializeField]
    ResultsScript resultScript;

    //gem collections
    [Header("Gem Collections")]
    [SerializeField]
    Animator redImage;
    [SerializeField]
    Text redText;
    [SerializeField]
    Animator blueImage;
    [SerializeField]
    Text blueText;
    [SerializeField]
    Animator tealImage;
    [SerializeField]
    Text tealText;
    [SerializeField]
    Animator yellowImage;
    [SerializeField]
    Text yellowText;
    [SerializeField]
    Animator greenImage;
    [SerializeField]
    Text greenText;
    //streaks
    [Header("Streaks")]
    [SerializeField]
    Text streakText;
    [SerializeField]
    Animator streakAnim;

    [Header("Goals Text")]
    [SerializeField]
    Text goal1;
    [SerializeField]
    Text goal2;
    [SerializeField]
    Text goal3;

    [SerializeField]
    Text pointsText;

    bool goal1Reached;
    bool goal2Reached;
    bool goal3Reached;
    bool playAnim;

    private void Start()
    {
        myScore = gameObject.GetComponent<Text>();
        _score = Score();
        goal1.text = manageScoreScript.goal1.ToString("n0");
        goal2.text = manageScoreScript.goal2.ToString("n0");
        goal3.text = manageScoreScript.goal3.ToString("n0");



        //Streak(5);
        // AddGem("blue", 1);
    }
    public void AddScore(int score)
    {
        resultScript.isWaiting = true;
        add += score;
        _score = Score();
        pointsText.text = "+" + score.ToString("n0") + " Points";
        pointsText.gameObject.GetComponent<Animator>().Play("ComboMeter", 0, 0);
        StopCoroutine(_score);
        StartCoroutine(_score);
    }
    //tallying up the score
    public IEnumerator Score()
    {
        if (manageScoreScript.score < manageScoreScript.goal3)
        {
            if (!playAnim)
            {
                float temp = 1 - scoreBar.GetCurrentAnimatorStateInfo(0).normalizedTime;
                scoreBar.Play("TurnOn", 0, temp);
                scoreBarText.Play("TurnOn", 0, temp);
                playAnim = true;
            }
        }
        while (add > 0)
        {
            //if (PauseMenus.gamePaused)
            //    break;
            if (add > 1600)
            {
                add -= 128;
                manageScoreScript.score += 128;
            }
            if (add > 800)
            {
                add -= 64;
                manageScoreScript.score += 64;
            }
            if (add > 400)
            {
                add -= 32;
                manageScoreScript.score += 32;
            }
            if (add > 200)
            {
                add -= 16;
                manageScoreScript.score += 16;
            }
            else if (add > 150)
            {
                add -= 8;
                manageScoreScript.score += 8;
            }
            else if (add > 100)
            {
                add -= 4;
                manageScoreScript.score += 4;
            }
            else if (add > 50)
            {
                add -= 2;
                manageScoreScript.score += 2;
            }
            else if (add <= 50)
            {
                add -= 1;
                manageScoreScript.score += 1;
            }
            if (manageScoreScript.score >= manageScoreScript.goal1 && !goal1Reached)
            {
                goal1.color = Color.green;
                goal1.gameObject.GetComponent<Outline>().enabled = true;
                goal1.gameObject.GetComponent<Animator>().Play("GoalHit", 0, 0);
                goal1Reached = true;
                au.PlayOneShot(taDa, PauseMenus.SFXvolume * .3f);
            }
            else if (manageScoreScript.score >= manageScoreScript.goal2 && !goal2Reached)
            {
                goal2.color = Color.green;
                goal2.gameObject.GetComponent<Outline>().enabled = true;
                goal2.gameObject.GetComponent<Animator>().Play("GoalHit", 0, 0);
                goal2Reached = true;
                au.PlayOneShot(taDa, PauseMenus.SFXvolume * .3f);
            }
            else if (manageScoreScript.score >= manageScoreScript.goal3 && !goal3Reached)
            {
                goal3.color = Color.green;
                goal3.gameObject.GetComponent<Outline>().enabled = true;
                goal3.gameObject.GetComponent<Animator>().Play("GoalHit", 0, 0);
                goal3Reached = true;
                au.PlayOneShot(taDa, PauseMenus.SFXvolume * .2f);
            }
            myScore.text = "Score: " + manageScoreScript.score.ToString("n0").Replace(manageScoreScript.score.ToString("n0"), "<color=#C5FFC3FF>" + manageScoreScript.score.ToString("n0") + "</color>");
            ScoreBar.UpdateBar();
            yield return new WaitForSeconds(.016f);
            //ending the game -Koester
            //if (manageScoreScript.PlayerTurns == 0 || manageScoreScript.countDownTime == 0)
            //     manageScoreScript.results.SetActive(true);//enabling the gameobject, reults, which ultimately ends the game -Koester

        }
        if (playAnim)
        {
            while (!scoreBar.GetCurrentAnimatorStateInfo(0).IsName("TurnOn"))
            {
                yield return null;
            }
            scoreBar.Play("TurnOff", 0, 0);
            scoreBarText.Play("TurnOff", 0, 0);
            playAnim = false;
        }
        resultScript.isWaiting = false;
    }

    public void Streak(int streakNumber)
    {
        streakAnim.Play("Streaks",0,0);
        streakText.text = "x"+streakNumber.ToString()+" Streak";
    }

    public void AddGem(string color,int value)
    {
        switch(color)
        {
            case "red":
                redImage.Play("AddGem",0,0);
                manageScoreScript.totalRed += value;
                redText.text = "x" + manageScoreScript.totalRed.ToString();
                break;
            case "blue":
                blueImage.Play("AddGem", 0, 0);
                manageScoreScript.totalBlue += value;
                blueText.text = "x" + manageScoreScript.totalBlue.ToString();
                break;
            case "teal":
                tealImage.Play("AddGem", 0, 0);
                manageScoreScript.totalTeal += value;
                tealText.text = "x" + manageScoreScript.totalTeal.ToString();
                break;
            //case "orange":
            //    orangeImage.Play("AddGem",0,0);
            //    manageScoreScript.totalOrage += value;
            //    orangeText.text = "x"+manageScoreScript.totalOrange.ToString();
            //    break;
            case "yellow":
                yellowImage.Play("AddGem", 0, 0);
                manageScoreScript.totalYellow += value;
                yellowText.text = "x" + manageScoreScript.totalYellow.ToString();
                break;
            case "green":
                greenImage.Play("AddGem", 0, 0);
                manageScoreScript.totalGreen += value;
                greenText.text = "x"+manageScoreScript.totalGreen.ToString();
                break;
            //case "purple":
            //    purpleImage.Play("AddGem",0,0);
            //    manageScoreScript.totalPurple += value;
            //    redText.text = "x"+manageScoreScript.totalPurple.ToString();
            //    break;
            default:
                Debug.LogError("Unrecognizable color. Be sure you're not using any capitalization.");
                break;
        }
        au.PlayOneShot(collectSound,PauseMenus.SFXvolume);
    }
}

