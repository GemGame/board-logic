using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {
    public static Image bar;
    [SerializeField]
    Text _percentage;
    [SerializeField]
    Image _bar;
    public static Text percentage;

    static ManageScore manageScoreScript;
    [SerializeField]
    ManageScore _manageScoreScript;


    private void Awake()
    {
        bar = _bar;
        percentage = _percentage;
        manageScoreScript = _manageScoreScript;
        bar.fillAmount = bar.fillAmount = 0;
        percentage.text = Mathf.Floor(0).ToString() + "%";
    }

    public static void UpdateBar()
    {
    bar.fillAmount = (float)manageScoreScript.score / manageScoreScript.goal3;
    percentage.text = Mathf.Floor(bar.fillAmount * 100).ToString() + "%";

    }
}
