using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScore : MonoBehaviour {
    public int score = 0;
    public int goal1 = 4000;
    public int goal2 = 8000;
    public int goal3 = 12000;
    public int streaks = 0;
    public int combos = 0;
    public static int largestStreak = 0;
    public static int highestScore = 0;
    public static int largestCombo = 0;

    public int completedGoals;
    public int totalGoals;
    public int destroyedTopaz = 0;
    public int destroyedRuby = 0;
    public int destroyedEmerald = 0;
    public int destroyedsaphire = 0;
    public int destroyedAmethyst = 0;
}
