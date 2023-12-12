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

    public Card(Card copy)
    {
        this.CardName = copy.CardName;
        this.Description = copy.Description;
        this.Image = copy.Image;
        this.TilePrefab = copy.TilePrefab;
    }

    public Card(string cardName, string description, Sprite image, GameObject tilePrefab)
    {
        CardName = cardName;
        Description = description;
        Image = image;
        TilePrefab = tilePrefab;
    }
}
