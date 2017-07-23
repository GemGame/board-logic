using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class defaultGem : baseGem {

	public override void PreDestroy()
    {
        //Debug.Log("PreDestroy()");
    }

    public override void PostDestroy()
    {
        //Debug.Log("PostDestroy");
    }
}
