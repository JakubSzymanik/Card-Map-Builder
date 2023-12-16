using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public string CardName;
    public string Description;
    public Sprite Image;
    public GameObject TilePrefab;
    public List<StatEffect> StatEffects;
    
    public Card(Card copy)
    {
        this.CardName = copy.CardName;
        this.Description = copy.Description;
        this.Image = copy.Image;
        this.TilePrefab = copy.TilePrefab;
        this.StatEffects = copy.StatEffects;
    }

    public Card(string cardName, string description, Sprite image, GameObject tilePrefab, List<StatEffect> effects)
    {
        CardName = cardName;
        Description = description;
        Image = image;
        TilePrefab = tilePrefab;
        StatEffects = effects;
    }
}
