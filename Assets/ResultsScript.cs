using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsScript : MonoBehaviour {
    [SerializeField]
    GameObject Star1;
    [SerializeField]
    GameObject Star2;
    [SerializeField]
    GameObject Star3;
    [SerializeField]
    Text infoText;
    [SerializeField]
    Text highScoresText;
    [SerializeField]
    Text levelCompletedText;
    [SerializeField]
    ManageScore manageScoreScript;
    public bool isWaiting; //waiting to run the coroutine -- this is determiend by the "adding score" script


    void OnEnable()
    {
        SaveLoadPrefs.Load();
        StartCoroutine(Display());
    }
    IEnumerator Display()
    {
        while (isWaiting)
            yield return null;
        yield return new WaitForSeconds(3f);
        while (isWaiting)
            yield return null;
        if (manageScoreScript.score > ManageScore.highestScore)
            ManageScore.highestScore = manageScoreScript.score;
        gameObject.GetComponent<Animator>().Play("Victory");
        //here we are displayig all game information
        highScoresText.text = "High Scores: " + ManageScore.highestScore.ToString("n0");

        if (manageScoreScript.score > manageScoreScript.goal1)
            levelCompletedText.text = "Level Complete";
        else
            levelCompletedText.text = "Game Over";

        infoText.text = "	Goals Completed " + manageScoreScript.completedGoals + "/" + manageScoreScript.totalGoals + "\r\n"
            + "\r\n"
            + "Difficulty: " + PauseMenus.difficulty + "\r\n"
            + "Stars Earned: " + "2" + "\r\n"
            + "Total Stars: " + "6" + "\r\n"
            + "Highest Combo: " + manageScoreScript.combos + "\r\n"
            + "Largest Streak: " + manageScoreScript.streaks + "\r\n"
            + "\r\n"
            + "		Score: " + manageScoreScript.score.ToString("n0") + "\r\n";
    }
}
