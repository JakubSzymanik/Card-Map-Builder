using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//obsolete for now
[CreateAssetMenu(menuName = "Items/Stats Level Service", fileName = "New Stats Level Service")]
public class StatsLevelsService : ScriptableObject
{
    [SerializeField] StatRelations relations;

    public int GetStatValue(int targetValue, StatType type, out StatType maxType, List<StatValue> stats)
    {
        var relation = relations.relations.Find(v => v.fluidStatType == type);
        
        if (relation == null)
        {
            maxType = null;
            return targetValue;
        }

        maxType = relation.maxStatType;
        return Mathf.Clamp(targetValue, 0, stats.Find(v => v.Type == relation.maxStatType).Value);
    }
}
