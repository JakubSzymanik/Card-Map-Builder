using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatsHandler : MonoBehaviour
{ 
    //[SerializeField] StatPanel statPanel;
    [SerializeField] StatStreamHandler statStreamHandler;
    [SerializeField] List<StatValue> stats = new List<StatValue>();
    
    List<StatEffect> thisTurnEffects = new List<StatEffect>();

    public void AddStatEffects(List<StatEffect> effects)
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

            statStreamHandler.PublishStatChange(new StatValueDTO
            {
                Type = effect.Type,
                Value = totalValue,
                PreviousValue = previousValue,
                Combo = comboValue
            });
        }
    }

    public void EndTurn()
    {
        thisTurnEffects = new List<StatEffect>();
    }
}
