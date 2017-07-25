using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBar : MonoBehaviour {
    public static Transform bar;

    private void Start()
    {
        bar = gameObject.transform;
    }

    public static void UpdateBar()
    {
        try
        {
            bar.localScale = new Vector3((float)ManageScore.score / ManageScore.goal3, 1, 1);
            print((float)(float)ManageScore.score / ManageScore.goal3);
        }
        catch
        {
            return;
        }
    }
}
