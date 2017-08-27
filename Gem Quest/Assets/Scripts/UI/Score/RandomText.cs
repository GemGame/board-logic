using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour {

    [SerializeField]
    Text text1;
    [SerializeField]
    Text text2;
    [SerializeField]
    Text text3;

    private void Start()
    {
        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();
        text3 = GameObject.Find("Text3").GetComponent<Text>();
    }

    public void Congradulate()
    {
        RandomTextGenerator rtg = new RandomTextGenerator();
        string message = rtg.GetRandomStatement();
        int temp = Random.Range(0, 3);

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
        }

    }
}
