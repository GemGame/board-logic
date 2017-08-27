using UnityEngine;
public class RandomTextGenerator {

    string[] randomStatements = { "Excellent", "Outstanding", "Awesome", "Kudos" };
	public RandomTextGenerator()
    {

    }
    public string GetRandomStatement()
    {
        int randomIndex = Random.Range(0, randomStatements.Length);
        return randomStatements[randomIndex];
    }

}
