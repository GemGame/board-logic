using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenEffect : MonoBehaviour {
    public static bool resetLevel;

    // Use this for initialization
    void  OnEnable()
    {
        if (resetLevel)
        {
            gameObject.GetComponent<Animator>().Play("FillOut", 0, 0);
            gameObject.GetComponent<Transform>().transform.rotation = Quaternion.Euler(0, 180, 0);
            Time.timeScale = 0;
            StartCoroutine(Resetting());
        }
	}
    IEnumerator Resetting()
    {
        print("true");
        yield return new WaitForSecondsRealtime(1.1f);
        gameObject.GetComponent<Transform>().transform.rotation = Quaternion.Euler(0, 0, 0);
        resetLevel = false;
        Time.timeScale = PauseMenus.scaleTime;
        PauseMenus.gamePaused = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
