using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour {

    [SerializeField]
    Text text1;
    [SerializeField]
    Text text2;
    [SerializeField]
    Text text3;

    public void Cogratulate(string message)
    {

        int temp = Random.Range(0, 3);
        print(temp + " " + message);
        switch (temp)
        {
            default:
                text1.gameObject.GetComponent<Animator>().Play("Text",0,0);
                text1.text = message;
                break;
            case 1:
                text2.gameObject.GetComponent<Animator>().Play("Text", 0, 0);
                text2.text = message;
                break;
            case 2:
                text3.gameObject.GetComponent<Animator>().Play("Text", 0, 0);
                text3.text = message;
                break;
        }
    }
}
