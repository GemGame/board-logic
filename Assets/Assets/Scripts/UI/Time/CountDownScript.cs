using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownScript : MonoBehaviour {

    float countDownTimer = 60;
    float miliSeconds;
    [Range (.02f,10f)]
    public float countDownRate;
    Text myText;
    Outline myOutline;

    [Header ("Outine Color")]
    [SerializeField]
    Color start;
    [SerializeField]
    Color end;
    [Header("Text Color")]
    [SerializeField]
    Color _start;
    [SerializeField]
    Color _end;
    [SerializeField]
    AudioSource sounds;
    [SerializeField]
    AudioClip tick;


    private void Start()
    {
        myText = gameObject.GetComponent<Text>();
        myOutline = gameObject.GetComponent<Outline>();
        StartCoroutine(CountDown());
    }

    public void AddCountDown(float addTime)
    {
        countDownTimer = addTime;
    } 
    //counting down until the game is over
    public IEnumerator CountDown()
    {
        float placeholder = countDownTimer;
        while (countDownTimer > 0 || miliSeconds > 0)
        {
            if (PauseMenus.gamePaused)
                break;
            yield return new WaitForSeconds(countDownRate);
            if (miliSeconds > 0)
                miliSeconds -= 1;
            else if (miliSeconds <= 0 && countDownTimer > 0)
            {
                miliSeconds = 10f;
                countDownTimer -= 1;
                if (countDownTimer > 10)
                    gameObject.GetComponent<Animator>().Play("Subtract", 0, 0);
                else
                    gameObject.GetComponent<Animator>().Play("DangerouslyLow", 0, 0);
                if (countDownTimer <= 10)
                    sounds.PlayOneShot(tick, PauseMenus.SFXvolume);
            }
            else if (miliSeconds <= 0 && countDownTimer <= 0)
            {
                print("game over");
                //show score
               //end game
            }
            //myText.text = string.Format("{0}.{1}", countDownTimer, (int)miliSeconds);
            myText.text = countDownTimer.ToString();
            Color lerpedOutline = Color.Lerp(end, start, countDownTimer/placeholder);
            Color lerpedFont = Color.Lerp(_end, _start, countDownTimer / placeholder);
            myOutline.effectColor = lerpedOutline;
            myText.color = lerpedFont;
        }
    }
}
