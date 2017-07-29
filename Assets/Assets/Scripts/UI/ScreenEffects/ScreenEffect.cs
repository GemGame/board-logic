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
            resetLevel = false;
            gameObject.GetComponent<Animator>().Play("FillOut", 0, 0);
            gameObject.GetComponent<Transform>().transform.rotation = Quaternion.Euler(0, 180, 0);
            Time.timeScale = 0;
            StartCoroutine(Resetting());
        }
	}
    IEnumerator Resetting()
    {
        yield return new WaitForSecondsRealtime(1.1f);
        gameObject.GetComponent<Transform>().transform.rotation = Quaternion.Euler(0, 0, 0);
        Time.timeScale = PauseMenus.scaleTime;
        PauseMenus.gamePaused = false;
    }
}
