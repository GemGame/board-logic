using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class upgradeController
{
    gemUpgrade[] gemUpgrades;

    public upgradeController(gemUpgrade[] upgrades)
    {
        gemUpgrades = upgrades;
        SortGemUpgrades();  //Just in case
    }

    void SortGemUpgrades()
    {
        
            int length = gemUpgrades.Length;
            int potentialNewIndex = 0;
            gemUpgrade tempUpgrade;
            for (int moveIndex = 0; moveIndex < length; moveIndex++)    //For each move
            {
                potentialNewIndex = moveIndex;
                for (int newMove = moveIndex; newMove < length; newMove++)    //For eah remaining move
                {
                    if (gemUpgrades[potentialNewIndex].comboCount < gemUpgrades[newMove].comboCount)    //Compare items
                    {
                        potentialNewIndex = newMove;
                    }
                }
                tempUpgrade = gemUpgrades[moveIndex];   //Store copy of upgrade
                gemUpgrades[moveIndex] = gemUpgrades[potentialNewIndex];  //Try update upgrade
                gemUpgrades[potentialNewIndex] = tempUpgrade;   //Swap places
            }
            //return move;
        
    }

    public List<boardSquare> GetRandomUpgradedGemList(List<boardSquare> moveList)
    {
        List<boardSquare> gemsToUpgrade = new List<boardSquare>();
        int moveCount = moveList.Count;
        int upgradeCount = 0;
        
        for(int x = 0; x < gemUpgrades.Length; x++)
        {
            if (moveCount >= gemUpgrades[x].comboCount)
                upgradeCount = gemUpgrades[x].gemReward;
        }
        
        //Debug.Log(upgradeCount);
        for (int x = 0; x < upgradeCount; x++)
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
