using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Deck : MonoBehaviour
{
    [SerializeField] private DeckData deckData;
    List<Card> cards = new List<Card>();

    private void Start()
    {
        for(int i = 0; i < deckData.Cards.Count; i++) 
        {
            var cd = deckData.Cards[i].card;
            cards.Add(new Card(cd));
        }
        Shuffle();
    }
    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 1; i--)
        {
            int rnd = Random.Range(0, i + 1);
            var temp = cards[rnd];
            cards[rnd] = cards[i];
            cards[i] = temp;
        }
    }

    public Card Draw()
    {
        if (cards.Count <= 0) return null;

        var card = cards[cards.Count - 1]; 
        cards.RemoveAt(cards.Count - 1);
        return card;
    }
}
