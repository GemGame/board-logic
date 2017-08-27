using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCreationEffect : MonoBehaviour
{
    //[SerializeField]
    //GameObject creationEffectPrefab;
    //[SerializeField]
    //Animator myAnim;
    defaultGem gem;


    // Use this for initialization
    void Awake () {
        gem = gameObject.GetComponent<defaultGem>();
        gem.mySprite.Play("ChargeUp",0,1);
        print(gem.mySprite);
		
	}
}
