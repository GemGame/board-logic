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
        Debug.Log("Gem upgrades instantiated to " + gemUpgrades.Length);
        SortGemUpgrades();  //Just in case
    }

    void SortGemUpgrades()  //Just in case designers suck
    {

        int length = gemUpgrades.Length;
        int potentialNewIndex = 0;
        gemUpgrade tempUpgrade;
        for (int moveIndex = 0; moveIndex < length; moveIndex++)    //For each move
        {
            potentialNewIndex = moveIndex;
            for (int newMove = moveIndex; newMove < length; newMove++)    //For eah remaining move
            {
                if (gemUpgrades[potentialNewIndex].comboCount >= gemUpgrades[newMove].comboCount)    //Compare items
                {
                    potentialNewIndex = newMove;
                }
            }
            tempUpgrade = gemUpgrades[moveIndex];   //Store copy of upgrade
            gemUpgrades[moveIndex] = gemUpgrades[potentialNewIndex];  //Try update upgrade
            gemUpgrades[potentialNewIndex] = tempUpgrade;   //Swap places
        }
    }

    int GetUpgradeCount(int moveListCount)
    {
        int upgradeCount = 0;
        for (int x = 0; x < gemUpgrades.Length; x++) //For each upgrade
        {
            if (moveListCount >= gemUpgrades[x].comboCount)
                upgradeCount = gemUpgrades[x].gemReward;
        }
        return upgradeCount;
    }

    List<boardSquare> ListMinusMaxedSquares(List<boardSquare> list)
    {
        List<boardSquare> tempList = new List<boardSquare>();
        int count = list.Count;
        for(int x = 0; x < count; x++)
        {
            if(list[x].GemScript.UpgradedPrefab != null)
            {
                tempList.Add(list[x]);
            }
        }
       
        return tempList;
    }
    public List<boardSquare> GetRandomUpgradedGemList(List<boardSquare> moveList)
    {
        List<boardSquare> gemsToUpgrade = new List<boardSquare>();
        int moveCount = moveList.Count;
        int upgradeCount = GetUpgradeCount(moveCount);

        Debug.Log("Upgrade count " + upgradeCount);

        moveList = ListMinusMaxedSquares(moveList); //Remove squares where upgraded prefab == null

        for (int x = 0; x < upgradeCount; x++)
        {
            //int index = Random.Range(0, moveList.Count);
            
            //              -- OR --

            int index = PreventDuplicateEntries(gemsToUpgrade, moveList, Random.Range(0, moveList.Count));

            if (index != -1)    //If index didn't error out in PreventDuplicateEntries()
                gemsToUpgrade.Add(moveList[index]);
            //Debug.Log(moveList[index]);
            //moveList.Remove(moveList[index]);
        }

        Debug.Log("Upgrade controller returned " + gemsToUpgrade.Count + " upgrades.");

        return gemsToUpgrade;
    }
    bool UniqueMovesRemain(List<boardSquare> upgradeList, List<boardSquare> moveList, int index)
    {
        bool movesRemain = false;
        foreach (boardSquare bs in moveList)
        {
            if (!upgradeList.Contains(bs))
            {
                movesRemain = true;
            }
        }
        return movesRemain;
    }
    int PreventDuplicateEntries(List<boardSquare> upgradeList, List<boardSquare> moveList, int index)   //Returns random valid index. -1 denotes no index available
    {
        if (UniqueMovesRemain(upgradeList, moveList, index))    //Get unique move first if able
            if (upgradeList.Contains(moveList[index])) //Don't allow duplicate entries
            {
                index = Random.Range(0, moveList.Count);
                return PreventDuplicateEntries(upgradeList, moveList, index);   //Recurse and try again
            }
        else if (moveList[index].GemScript.UpgradedPrefab != null)   //If index can upgrade
            return index;
        return -1;  //Return error
    }
}
