//Written By Christopher Cooke
//Gem Quest Animation Manager
//Controls gems falling to empty squares
//Sets board square flags
using System.Collections.Generic;
using UnityEngine;

public class animationManager : MonoBehaviour {
     
    //Public Variables
    float gemFallingSpeed = 0.5f;

    //Private Variables
    List<boardSquare> animationsPlaying = new List<boardSquare>();
    //bool playingAnims = false;

    //Properties
    public float GemFallingSpeed { set { gemFallingSpeed = value; } }
    
    //Methods
    public bool CheckAnimationsPlaying()
    {
        if(animationsPlaying != null)
        {
            for (int x = 0; x < animationsPlaying.Count; x++)
            {

                if(animationsPlaying[x].AnimPlaying)
                {                    
                    //Debug.Log("Animation " + x + " still playing");
                    return true;
                }
                else {  }
            }
        }
        return false;
    }
    public void PlayFallingAnimations(List<boardSquare> squares)
    {
        //animationsPlaying = new bool[squares.Count];
        foreach(boardSquare square in squares)
        {            
           // Debug.Log("Starting coroutine for index " + squares.IndexOf(square));
            StartCoroutine(GemFallingAnimation(square, squares.IndexOf(square)));
        }
    }
    IEnumerator<boardSquare> GemFallingAnimation(boardSquare square, int index)
    {
        
        //square.Gem.transform.localPosition = new Vector3(0, 0, 0);
        if (square != null && square.Gem != null && !square.AnimPlaying)
        {
            square.AnimPlaying = true;
            animationsPlaying.Add(square);
            //animationsPlaying[index] = true;

            //Debug.Log("Started animation on square " + square.Gem);
            while (square.Gem != null && square.Gem.transform.localPosition != Vector3.zero)
            {
               
                //squaresDoneFalling = false;
                //Debug.Log(square + " is falling");
                square.Gem.transform.localPosition = Vector3.MoveTowards(square.Gem.transform.localPosition, Vector3.zero, gemFallingSpeed * Time.deltaTime);
                yield return null;
            }
            //Debug.Log("Changing index " + index + "back to false.");
            //animationsPlaying[index] = false;
            animationsPlaying.Remove(square);
            square.AnimPlaying = false;
            //squaresDoneFalling = true;
        }
    }
}
