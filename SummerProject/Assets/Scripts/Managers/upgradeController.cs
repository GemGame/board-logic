using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeController {

	public List<boardSquare> GetRandomUpgradedGemList(List<boardSquare> moveList)
    {
        List<boardSquare> gemsToUpgrade = new List<boardSquare>();
        int upgradeCount = 0;

        if(moveList.Count >= 12)
        {
            upgradeCount = 7;
        }
        else if(moveList.Count >= 11)
        {
            upgradeCount = 6;
        }
        else if (moveList.Count >= 10)
        {
            upgradeCount = 5;
        }
        else if (moveList.Count >= 9)
        {
            upgradeCount = 4;
        }
        else if (moveList.Count >= 7)
        {
            upgradeCount = 3;
        }
        else if (moveList.Count >= 5)
        {
            upgradeCount = 2;
        }
        else if (moveList.Count >= 3)
        {
            upgradeCount = 1;
        }
        //Debug.Log(upgradeCount);
        for(int x = 0; x < upgradeCount; x++)
        {
            int index = Random.Range(0, moveList.Count);
            while (gemsToUpgrade.Contains(moveList[index]))
            {
                index = Random.Range(0, moveList.Count);
            }
            gemsToUpgrade.Add(moveList[index]);
            //Debug.Log(moveList[index]);
            //moveList.Remove(moveList[index]);
        }

        return gemsToUpgrade;
    }
}
