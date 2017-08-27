using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour {

    [SerializeField]
    Text text1;
    [SerializeField]
    Text text2;
    [SerializeField]
    Text text3;

    public void Congradulate()
    {
        RandomTextGenerator rtg = new RandomTextGenerator();
        string message = rtg.GetRandomStatement();
        int temp = Random.Range(0, 3);
        /*
        switch (temp)
        {
            default:
                text1.text = message;
                text1.gameObject.GetComponent<Animator>().Play("Text",0,0);
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
        */
    }
}
