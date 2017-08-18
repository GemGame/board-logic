//Written By Christopher Cooke
//Gem Quest Upgrade
//How many gems must be in a combo to get this reward?  
using UnityEngine;

[System.Serializable]
public class gemUpgrade
{
    [SerializeField]
    public int comboCount = 0;
    [SerializeField]
    public int gemReward = 0;

    public gemUpgrade(int combo, int reward)
    {
        comboCount = combo;
        gemReward = reward;
    }
}
