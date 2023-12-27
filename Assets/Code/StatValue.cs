using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatValue
{
    public StatType Type;
    public int Value;

    public StatValue(StatType type, int value)
    {
        Type = type;
        Value = value;
    }
}
