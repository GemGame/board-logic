using UnityEngine;
public class RandomTextGenerator {

    string[] randomStatements = { "Not too shabby!", "Not bad!", "Looking Good!", "Alright!", "Keep at it!", //poor combo score
            "Great!", "Nice!", "Well Done!", "Wow!", "Kudos!", "Awesome!", //decent combo score
            "Excellent!", "Outstanding!", "Astonishing!", "Amazing!", "Marvelous!", "Unbelievable!" }; //great combo score

	public RandomTextGenerator()
    {

    }
    public string GetRandomStatement(int score)
    {
        int randomIndex = Random.Range(11, randomStatements.Length);
        if(score >= 2000)//lvl 3 text
            randomIndex = Random.Range(11, randomStatements.Length);
        else if(score >= 1000)//lvl 2 text
            randomIndex = Random.Range(5, 10);
        else//lvl 1 text
            randomIndex = Random.Range(0, 4);
        return randomStatements[randomIndex];
    }
    public string GetStatementBasedOnScore(int score)
    {
        return null;
    }

}
