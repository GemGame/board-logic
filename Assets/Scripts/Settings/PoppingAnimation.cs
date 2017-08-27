using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppingAnimation : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        Animator myAnimator = GetComponent<Animator>();
        StartCoroutine(MyCor(myAnimator));
	}

    // Use this for initialization
    void OnDisable()
    {
        try
        {
            GameObject.Find("Canvas/Menus").GetComponent<LobbyMenus>().Whoop();
        }
        catch
        {
        }
    }

    IEnumerator MyCor(Animator myAnimator)
    {
        GameObject.Find ("Canvas/Menus").GetComponent<LobbyMenus>().Pop();
        myAnimator.enabled = true;
        myAnimator.Play("Popping",0,0);
        yield return new WaitForSecondsRealtime(.334f);
        myAnimator.enabled = false;
    }


}
