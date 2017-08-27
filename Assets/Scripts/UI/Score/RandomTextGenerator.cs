using UnityEngine;
public class RandomTextGenerator {

    string[] randomStatements = { "Not too shabby!", "Not bad!", "Looking Good!", "Alright!", "Keep at it!", //poor combo score
            "Great!", "Nice!", "Well Done!", "Wow!", "Kudos!", "Awesome!", //decent combo score
            "Excellent!", "Outstanding!", "Astonishing!", "Amazing!", "Marvelous!", "Unbelievable!" }; //great combo score

	public RandomTextGenerator()
    {

    }
    public string GetRandomStatement()
    {
        int randomIndex = Random.Range(0, randomStatements.Length);
        return randomStatements[randomIndex];
    }
    public string GetStatementBasedOnScore(int score)
    {
        return null;
    }

}
