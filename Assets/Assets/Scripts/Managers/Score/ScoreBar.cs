using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour {
    public static Transform bar;
    [SerializeField]
    Transform goal1;
    [SerializeField]
    Transform goal2;
    [SerializeField]
    Transform goal3;
    static ManageScore manageScoreScript;


    private void Start()
    {
        bar = gameObject.transform;
        manageScoreScript = GameObject.Find("Canvas").GetComponent<ManageScore>();

        //Rects
        //RectTransform rt = bar.gameObject.GetComponent<RectTransform>();
        //RectTransform rt1 = goal1.gameObject.GetComponent<RectTransform>();
        //RectTransform rt2 = goal2.gameObject.GetComponent<RectTransform>();
        //RectTransform rt3 = goal3.gameObject.GetComponent<RectTransform>();

        //rt1.sizeDelta = rt.sizeDelta;
        //rt2.sizeDelta = rt.sizeDelta;
        //rt3.sizeDelta = rt.sizeDelta;
        //rt1.localPosition = rt.localPosition;
        //rt2.localPosition = rt.localPosition;
        //rt3.localPosition = rt.localPosition;

        //scales
        goal1.transform.localScale = new Vector3((float)manageScoreScript.goal1 / manageScoreScript.goal3, 1, 1);
        goal2.transform.localScale = new Vector3(1f - (float)manageScoreScript.goal2 / manageScoreScript.goal3, 1, 1);
        goal3.transform.localScale = new Vector3(((float)manageScoreScript.goal2 - (float)manageScoreScript.goal1) / manageScoreScript.goal3, 1, 1);

        //positions
        goal2.transform.localPosition = new Vector3(0, -16, 0);
        goal2.transform.localPosition = new Vector3(((300 / 2) - ((goal2.transform.localScale.x + goal1.transform.localScale.x) * 300f)), 0, 0);
        goal3.transform.localPosition = new Vector3(((300 / 2) - (goal1.transform.localScale.x * 300f)), 0, 0);
    }

    public static void UpdateBar()
    {
        try
        {
            bar.localScale = new Vector3((float)manageScoreScript.score / manageScoreScript.goal3, 1, 1);
        }
        catch
        {
            return;
        }
    }
}
