using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScore : MonoBehaviour {
    public int score = 0;
    public int goal1 = 16000;
    public int goal2 = 32000;
    public int goal3 = 50000;
    public int streaks = 0;
    public static int largestStreak = 0;
    public static int highesScore = 0;
    public int combos = 0;
    public static int largestCombo = 0;

    public int destroyedTopaz = 0;
    public int destroyedRuby = 0;
    public int destroyedEmerald = 0;
    public int destroyedsaphire = 0;
    public int destroyedAmethyst = 0;

    private void Start()
    {
        score = 0;
        streaks = 0;
        combos = 0;
        destroyedTopaz = 0;
        destroyedRuby = 0;
        destroyedEmerald = 0;
        destroyedsaphire = 0;
        destroyedAmethyst = 0;
    }


}
