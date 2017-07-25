using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSelect : MonoBehaviour {

    private void OnMouseEnter()
    {
        if (Time.timeScale > 0)
        {
            gameObject.transform.localScale = new Vector3(2.4f, 2.4f, 2.4f);
            print(gameObject.transform.localScale);
            Sound.SelectGem();
           // transform.parent.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
        print(gameObject.transform.localScale);
        //transform.parent.GetComponent<BoxCollider>().enabled = false;
    }
}
