using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour {

    [SerializeField]
    Text text1;
    [SerializeField]
    Text text2;
    [SerializeField]
    Text text3;
    [SerializeField]
    Text text4;
    [SerializeField]
    Text text5;
    [SerializeField]
    Text text6;

    int previousNumber = 1000;

    private void Start()
    {
        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();
        text3 = GameObject.Find("Text3").GetComponent<Text>();
        text4 = GameObject.Find("Text4").GetComponent<Text>();
        text5 = GameObject.Find("Text5").GetComponent<Text>();
        text6 = GameObject.Find("Text6").GetComponent<Text>();
    }

    public void Congradulate(int score)
    {
        RandomTextGenerator rtg = new RandomTextGenerator();
        string message = rtg.GetRandomStatement(score);
        int temp = Random.Range(0, 6);
        //checking to see if the temp variable was previously used, if so, we are assigning it a different value
        while (temp == previousNumber)
            temp = Random.Range(0, 6);
        previousNumber = temp;//memorizing the last value used for temp

        switch (temp)
        {
            default:
                text1.text = message;
                text1.gameObject.GetComponent<Animator>().Play("Text", 0, 0);
                break;
            case 1:
                text2.text = message;
                text2.gameObject.GetComponent<Animator>().Play("Text", 0, 0);
                break;
            case 2:
                text3.text = message;
                text3.gameObject.GetComponent<Animator>().Play("Text", 0, 0);
                break;
            case 3:
                text4.text = message;
                text4.gameObject.GetComponent<Animator>().Play("Text", 0, 0);
                break;
            case 4:
                text5.text = message;
                text5.gameObject.GetComponent<Animator>().Play("Text", 0, 0);
                break;
            case 5:
                text6.text = message;
                text6.gameObject.GetComponent<Animator>().Play("Text", 0, 0);
                break;
        }

    }
}
