﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScore : MonoBehaviour {
    public enum GameType {Arcade, Quest};
    public GameType gameType;
    public enum GameRules { TimeAttack, TurnBase };
    public GameRules gameRules;
    [HideInInspector]
    public float gameLength = 0;//length of game
    public int countDownTime = 60;//length of game
    [HideInInspector]
    public int PlayerTurns = 1;//current turns for player
    public int startingTurns = 8;//the startig turns for player

    public int score = 0;
    public int goal1 = 4000;
    public int goal2 = 8000;
    public int goal3 = 12000;
    public int streaks = 0;
    public int combos = 0;

    public static int highestScore = 2428;

    [HideInInspector]
    public int completedGoals;
    public int totalGoals;
    public int totalYellow = 0;
    public int totalRed = 0;
    public int totalGreen = 0;
    public int totalOrange = 0;
    public int totalPurple = 0;
    public int totalBlue = 0;
    public int totalTeal = 0;

    public GameObject results;

    private void Start()
    {
        ResultsScript.isGameOver = false;
        PlayerTurns = startingTurns;
        gameLength = Time.time;
    }
}
