using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSelect : MonoBehaviour
{

    public float scaleIncreasePercent = .2f;
    Vector3 currentScale;
    Vector3 oldScale;

    private void Start()
    {
        currentScale = this.transform.localScale;
        oldScale = currentScale;
    }
    private void OnMouseEnter()
    {
        if (Time.timeScale > 0) //What is this for? - CC
        {
            currentScale += (currentScale * scaleIncreasePercent);
            gameObject.transform.localScale = currentScale;
            //print(gameObject.transform.localScale);
            Sound.SelectGem();
            // transform.parent.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnMouseExit()
    {
        currentScale = oldScale;
        gameObject.transform.localScale = currentScale;
        //print(gameObject.transform.localScale);
        //transform.parent.GetComponent<BoxCollider>().enabled = false;
    }
}
