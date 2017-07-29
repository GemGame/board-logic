using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddingScore : MonoBehaviour
{
    int score;
    int add;
    [SerializeField]
    Text myScore;
    IEnumerator _score;
    [SerializeField]
    AudioSource au;
    [SerializeField]
    AudioClip taDa;
    [SerializeField]
    Animator scoreBar;
    [SerializeField]
    Animator scoreBarText;
    [SerializeField]
    ManageScore manageScoreScript;

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
        AddScore(50000);
        goal1.text = manageScoreScript.goal1.ToString("n0");
        goal2.text = manageScoreScript.goal2.ToString("n0");
        goal3.text = manageScoreScript.goal3.ToString("n0");
    }

    public void AddScore(int score)
    {
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
            scoreBar.Play("TurnOn", 0, 0);
            scoreBarText.Play("TurnOn", 0, 0);
            playAnim = true;
        }
        while (add > 0)
        {
            if (PauseMenus.gamePaused)
                break;
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
                au.PlayOneShot(taDa, PauseMenus.SFXvolume*.2f);
            }
            myScore.text = "Score: " + manageScoreScript.score.ToString("n0").Replace(manageScoreScript.score.ToString("n0"), "<color=#C5FFC3FF>" + manageScoreScript.score.ToString("n0") + "</color>");
            ScoreBar.UpdateBar();
            yield return new WaitForSeconds(.016f);
        }
        if (playAnim)
        {
            scoreBar.Play("TurnOff", 0, 0);
            scoreBarText.Play("TurnOff", 0, 0);
            playAnim = false;
        }
    }
}

