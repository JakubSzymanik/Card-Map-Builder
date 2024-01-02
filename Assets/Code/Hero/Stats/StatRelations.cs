using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Stat Relations", fileName = "New Stat Relations")]
public class StatRelations : ScriptableObject
{
    public List<StatRelation> relations;
}
