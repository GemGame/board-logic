using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsTextScript : MonoBehaviour
{
    bool title = true; 

	public void ChangeCredits ()
    {
        if (title)
            title = false;
        else
            title = true;

        switch (title)
        {
            case true:
                Transform temp = transform.Find("G");
                temp.gameObject.GetComponent<Text>().text = "G";
                temp = transform.Find("e");
                temp.gameObject.GetComponent<Text>().text = "e";
                temp = transform.Find("m");
                temp.gameObject.GetComponent<Text>().text = "m";
                temp = transform.Find("Q");
                temp.gameObject.GetComponent<Text>().text = "Q";
                temp = transform.Find("u");
                temp.gameObject.GetComponent<Text>().text = "u";
                temp = transform.Find("e(1)");
                temp.gameObject.GetComponent<Text>().text = "e";
                temp = transform.Find("s");
                temp.gameObject.GetComponent<Text>().text = "s";
                temp = transform.Find("t");
                temp.gameObject.GetComponent<Text>().text = "t";
                break;
            case false:
                temp = transform.Find("G");
                temp.gameObject.GetComponent<Text>().text = "C";
                temp = transform.Find("e");
                temp.gameObject.GetComponent<Text>().text = "r";
                temp = transform.Find("m");
                temp.gameObject.GetComponent<Text>().text = "e";
                temp = transform.Find("Q");
                temp.gameObject.GetComponent<Text>().text = "d";
                temp = transform.Find("u");
                temp.gameObject.GetComponent<Text>().text = "i";
                temp = transform.Find("e(1)");
                temp.gameObject.GetComponent<Text>().text = "t";
                temp = transform.Find("s");
                temp.gameObject.GetComponent<Text>().text = "s";
                temp = transform.Find("t");
                temp.gameObject.GetComponent<Text>().text = " ";
                break;
          
        }
    }

}
