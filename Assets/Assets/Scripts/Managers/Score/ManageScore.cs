using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScore : MonoBehaviour {
    public enum GameType {Arcade, Quest};
    public GameType gameType;
    public enum GameRules { TimeAttack, TurnBase };
    public GameRules gameRules;
    int gameLength = 0;//length of game
    public int countDownTime = 60;//length of game
    public int PlayerTurns = 8;//current turns for player
    public int startingTurns = 8;//the startig turns for player

    public int score = 0;
    public int goal1 = 4000;
    public int goal2 = 8000;
    public int goal3 = 12000;
    public int streaks = 0;
    public int combos = 0;

    public static int highestScore = 2428;

    public int completedGoals;
    public int totalGoals;
    public int destroyedTopaz = 0;
    public int destroyedRuby = 0;
    public int destroyedEmerald = 0;
    public int destroyedsaphire = 0;
    public int destroyedAmethyst = 0;

    public GameObject results;

    private void Start()
    {
        ResultsScript.isGameOver = false;
        PlayerTurns = startingTurns;
    }
}
