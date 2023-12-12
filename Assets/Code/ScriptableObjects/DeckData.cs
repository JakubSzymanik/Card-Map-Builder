using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Deck", fileName = "New Deck")]
public class DeckData : ScriptableObject
{
    public List<CardData> Cards;
}
