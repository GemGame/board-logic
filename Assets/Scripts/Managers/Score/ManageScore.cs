using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScore : MonoBehaviour {
    public enum GameType { Arcade, Quest };
    public GameType gameType;
    public enum GameRules { TimeAttack, TurnBase };
    public GameRules gameRules;
    [HideInInspector]
    public float gameLength = 0;//length of game
    public int countDownTime = 60;//length of game
    [HideInInspector]
    public int PlayerTurns = 1;//current turns for player
    public int startingTurns = 8;//the startig turns for player
    public string difficulty;

    public int score = 0;
    public int goal1 = 4000;
    public int goal2 = 8000;
    public int goal3 = 12000;
    public int streaks = 0;
    public int combos = 0;

    public static int highestScore = 2428;

    [HideInInspector]
    public int completedGoals;
    public int completedQuests;
    public int totalQuest;
    public static int totalStars;
    [HideInInspector]
    public int totalYellow = 0;
    [HideInInspector]
    public int totalRed = 0;
    [HideInInspector]
    public int totalGreen = 0;
    [HideInInspector]
    public int totalOrange = 0;
    [HideInInspector]
    public int totalPurple = 0;
    [HideInInspector]
    public int totalBlue = 0;
    [HideInInspector]
    public int totalTeal = 0;
    [Header("Maximum Goal for Gem Colors")]
    public int goalYellow = 10;
    public int goalRed = 10;
    public int goalGreen = 10;
    public int goalOrange = 10;
    public int goalPurple = 10;
    public int goalBlue = 10;
    public int goalTeal = 10;

    public GameObject results;

    private void Start()
    {
        ResultsScript.isGameOver = false;
        PlayerTurns = startingTurns;
        gameLength = Time.time;
        difficulty = PauseMenus.difficulty.ToString();

        //setting game time and turns
        if (gameType == GameType.Arcade)
           // if(gameRules == GameRules.TimeAttack)
                switch(PauseMenus.difficulty)
                {
                    case PauseMenus.Difficulty.Easy:
                        countDownTime = 120;
                        startingTurns = 16;
                    break;
                    case PauseMenus.Difficulty.Normal:
                        countDownTime = 90;
                        startingTurns = 12;
                    break;
                    case PauseMenus.Difficulty.Hard:
                        countDownTime = 60;
                        startingTurns = 8;
                    break;
                }
    }
}
