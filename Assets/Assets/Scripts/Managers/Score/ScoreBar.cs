using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour {
    public static Image bar;
    [SerializeField]
    Text _percentage;
    public static Text percentage;

    static ManageScore manageScoreScript;


    private void Start()
    {
        bar = gameObject.GetComponent<Image>();
        percentage = _percentage;
        manageScoreScript = GameObject.Find("Canvas").GetComponent<ManageScore>();
    }

    public static void UpdateBar()
    {
      bar.fillAmount = (float)manageScoreScript.score / manageScoreScript.goal3;
      percentage.text = Mathf.Floor(bar.fillAmount * 100).ToString()+"%";
    }
}
