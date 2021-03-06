﻿//Written By Christopher Cooke
//Gem Quest Animation Manager
//Controls gems falling to empty squares
//Sets board square flags
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationManager : MonoBehaviour
{
    //Public Variables
    float gemFallingSpeed = 0.5f;
    float gemFallingDelay = 1f;

    //Private Variables
    List<boardSquare> animationsPlaying = new List<boardSquare>();

    //Properties
    public float GemFallingSpeed { set { gemFallingSpeed = value; } }

    //Methods
    public bool CheckAnimationsPlaying()
    {
        if (animationsPlaying != null)
        {
            for (int x = 0; x < animationsPlaying.Count; x++)
            {
                if (animationsPlaying[x].AnimPlaying)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void PlayFallingAnimations(List<boardSquare> squares)
    {
        foreach (boardSquare square in squares)
        {
            StartCoroutine(GemFallingAnimation(square, squares.IndexOf(square)));
        }
    }
    public void PlayDelayedFallingAnimations(List<boardSquare> squares, float delay)
    {
        foreach (boardSquare square in squares)
        {
            StartCoroutine(DelayedGemFallingAnimation(square, squares.IndexOf(square), delay));
        }
    }

   
    IEnumerator GemFallingAnimation(boardSquare square, int index)
    {
        if (square != null && square.Gem != null && !square.AnimPlaying)
        {
            square.AnimPlaying = true;
            animationsPlaying.Add(square);

            while (square.Gem != null && square.Gem.transform.localPosition != Vector3.zero)
            {
                square.Gem.transform.localPosition = Vector3.MoveTowards(square.Gem.transform.localPosition, Vector3.zero, gemFallingSpeed * Time.deltaTime);
                yield return null;
            }
            animationsPlaying.Remove(square);
            square.AnimPlaying = false;
        }
    }
    IEnumerator DelayedGemFallingAnimation(boardSquare square, int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(GemFallingAnimation(square, index));
    }
}
