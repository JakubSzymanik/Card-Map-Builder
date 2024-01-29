using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatsHandler : MonoBehaviour
{ 
    [SerializeField] StatStreamHandler statStreamHandler;
    //[SerializeField] StatsLevelsService statsLevelsService;
    [SerializeField] List<StatValue> stats = new List<StatValue>();
    
    List<StatEffect> thisTurnEffects = new List<StatEffect>();
    List<StatEffect> temporaryStatEffects = new List<StatEffect>();

    public void AddPermanentStatEffects(List<StatEffect> effects) //stat effects from fields
    {
        if (effects == null || effects.Count == 0)
            return;

        foreach(StatEffect effect in effects)
        {
            StatValue stat = stats.Find(v => v.Type == effect.Type);
            if (stat == null) continue;

            int comboValue = thisTurnEffects.FindAll(v => v.Type == effect.Type).Count;
            int totalAddedValue = effect.Value + comboValue;
            int previousValue = stat.Value;
            int totalValue = stat.Value += totalAddedValue;

            //set connected stats - removing for now
            //statsLevelsService.GetStatLevels(stat, out int relativeStat, out StatValue higherStat);
            //if(higherStat != null)
            //{
            //    StatValue higherStatListRef = stats.Find(v => v.Type == higherStat.Type);
            //    int prevValue = higherStatListRef.Value;
            //    higherStatListRef.Value = higherStat.Value;

            //    statStreamHandler.PublishStatChange(new StatValueDTO
            //    {
            //        Type = higherStat.Type,
            //        Value = higherStat.Value,
            //        PreviousValue = prevValue,
            //        Combo = 0
            //    });
            //}

            statStreamHandler.PublishStatChange(new StatValueDTO
            {
                Type = effect.Type,
                Value = totalValue,
                PreviousValue = previousValue,
                Combo = comboValue
            });
        }
    }

    public void AddTemporaryStatEffects(List<StatEffect> effects)
    {

    }
    private void CleanTemporaryStatEffects()
    {

    }

    public void EndTurn()
    {
        thisTurnEffects = new List<StatEffect>();
    }
}
