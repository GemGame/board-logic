using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScript : MonoBehaviour {
    public static bool isGameOver;//this is set to true when game is over
    [SerializeField]
    Image Star1;
    [SerializeField]
    Image Star2;
    [SerializeField]
    Image Star3;
    [SerializeField]
    Text infoText;
    [SerializeField]
    Text highScoresText;
    [SerializeField]
    Text levelCompletedText;
    [SerializeField]
    ManageScore manageScoreScript;
    [SerializeField]
    GameObject HUDbuttons;
    public bool isWaiting; //waiting to run the coroutine -- this is determiend by the "adding score" script
    public static GameObject results;

    void OnEnable()
    {
        isGameOver = true;
        HUDbuttons.SetActive(false);
        SaveLoadPrefs.Load();
        StartCoroutine(MusicSettings.TurnOffMusic());
        StartCoroutine(Display());
    }
    void OnDestroy()
    {
        isGameOver = false;
    }
    IEnumerator Display()
    {
        float temp = Time.time - manageScoreScript.gameLength;
        print(Time.time);
        print(manageScoreScript.gameLength);
        print(Time.time - manageScoreScript.gameLength);
        if (temp >= 3600)
            temp = 3600;
        string format = (temp / 60).ToString("00") + ":" + (temp % 60).ToString("00");
        while (isWaiting)
            yield return null;
        yield return new WaitForSecondsRealtime(3f);
        if (manageScoreScript.score > ManageScore.highestScore)
            ManageScore.highestScore = manageScoreScript.score;
        gameObject.GetComponent<Animator>().Play("Victory");
        //here we are displayig all game information
        highScoresText.text = "High Scores: " + ManageScore.highestScore.ToString("n0");

        if(manageScoreScript.gameType == ManageScore.GameType.Arcade)
            levelCompletedText.text = "Game Over";
        else
            if (manageScoreScript.score > manageScoreScript.goal1)
                levelCompletedText.text = "Level Complete";
            else
                levelCompletedText.text = "Level Failed";
        //setting the stars
        if (manageScoreScript.score >= manageScoreScript.goal1)
        {
            Star1.sprite = Resources.Load<Sprite>("Art/Backgrounds/HUD/Images/Star");
        }
        if (manageScoreScript.score >= manageScoreScript.goal2)
        {
            Star2.sprite = Resources.Load<Sprite>("Art/Backgrounds/HUD/Images/Star");
        }
        if (manageScoreScript.score >= manageScoreScript.goal3)
        {
            Star3.sprite = Resources.Load<Sprite>("Art/Backgrounds/HUD/Images/Star");
        }


        infoText.text = "	Quests Completed " + manageScoreScript.completedQuests + "/" + manageScoreScript.totalQuest + "\r\n"
            + "\r\n"
            + "Game: "+manageScoreScript.gameRules + "\r\n"
            + "Difficulty: " + manageScoreScript.difficulty + "\r\n"
            + "Stars Earned: " + manageScoreScript.completedGoals+ "/" +3 + "\r\n"
            + "Total Stars: " + ManageScore.totalStars + "\r\n"
            + "Highest Combo: " + manageScoreScript.combos + "\r\n"
            + "Largest Streak: " + manageScoreScript.streaks + "\r\n"
            + "Total Time: " + format + "\r\n"
            + "\r\n"
            + "		Score: " + manageScoreScript.score.ToString("n0") + " Points"+"\r\n";
        //myText.text = string.Format("{0}.{1}", countDownTimer, (int)miliSeconds);
    }
}
