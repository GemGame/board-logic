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
                Transform temp = transform.FindChild("G");
                temp.gameObject.GetComponent<Text>().text = "G";
                temp = transform.FindChild("e");
                temp.gameObject.GetComponent<Text>().text = "e";
                temp = transform.FindChild("m");
                temp.gameObject.GetComponent<Text>().text = "m";
                temp = transform.FindChild("Q");
                temp.gameObject.GetComponent<Text>().text = "Q";
                temp = transform.FindChild("u");
                temp.gameObject.GetComponent<Text>().text = "u";
                temp = transform.FindChild("e(1)");
                temp.gameObject.GetComponent<Text>().text = "e";
                temp = transform.FindChild("s");
                temp.gameObject.GetComponent<Text>().text = "s";
                temp = transform.FindChild("t");
                temp.gameObject.GetComponent<Text>().text = "t";
                break;
            case false:
                temp = transform.FindChild("G");
                temp.gameObject.GetComponent<Text>().text = "C";
                temp = transform.FindChild("e");
                temp.gameObject.GetComponent<Text>().text = "r";
                temp = transform.FindChild("m");
                temp.gameObject.GetComponent<Text>().text = "e";
                temp = transform.FindChild("Q");
                temp.gameObject.GetComponent<Text>().text = "d";
                temp = transform.FindChild("u");
                temp.gameObject.GetComponent<Text>().text = "i";
                temp = transform.FindChild("e(1)");
                temp.gameObject.GetComponent<Text>().text = "t";
                temp = transform.FindChild("s");
                temp.gameObject.GetComponent<Text>().text = "s";
                temp = transform.FindChild("t");
                temp.gameObject.GetComponent<Text>().text = " ";
                break;
          
        }
    }

}
