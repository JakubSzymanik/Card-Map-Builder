using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatRelation
{
    public StatType baseStatType;
    public StatType resultingStatType;
    public List<int> levelRequirements = new List<int>();
    public int stepAfterMaxReq;

    public void GetValues(int totalBaseValue, out int relativeBaseValue, out int resultingLevel)
    {
        for(int i = 0; i < levelRequirements.Count; i++)
        {
            if (levelRequirements[i] > totalBaseValue)
            {
                int req = levelRequirements[i - 1];
                relativeBaseValue = totalBaseValue - req;
                resultingLevel = i - 1;
                return;
            }
        }

        int maxV = levelRequirements[levelRequirements.Count - 1];
        relativeBaseValue = (totalBaseValue - maxV) % stepAfterMaxReq;
        resultingLevel = Mathf.FloorToInt((totalBaseValue - maxV) / stepAfterMaxReq) + levelRequirements.Count;
    }
}
