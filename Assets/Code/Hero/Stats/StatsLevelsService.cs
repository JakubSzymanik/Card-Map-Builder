using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//obsolete for now
[CreateAssetMenu(menuName = "Items/Stats Level Service", fileName = "New Stats Level Service")]
public class StatsLevelsService : ScriptableObject
{
    [SerializeField] StatRelations relations;

    public void GetStatLevels(StatValue stat, out int relativeStat, out StatValue resultingHigherStat)
    {
        StatType baseType = stat.Type;
        StatRelation relation = relations.relations.Find(v => v.baseStatType == baseType);
        if(relation == null) { resultingHigherStat = null; relativeStat = 0; return; }

        relation.GetValues(stat.Value, out relativeStat, out int resultingLevel);
        resultingHigherStat = new StatValue(relation.resultingStatType, resultingLevel);
    }
}
