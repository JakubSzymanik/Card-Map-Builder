using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    [SerializeField] float cardWidth;
    [SerializeField] float space;
    [SerializeField] Transform midPoint;

    public void PlaceCards(List<CardInHand> cards, List<Card> newCards)
    { 
        float totalWidth = (cards.Count - 1) * (cardWidth + space);
        float startX = -totalWidth / 2;
        for(int i = 0; i < cards.Count; i++)
        {
            float relativeX = startX + i * (cardWidth + space);
            Vector3 pos = midPoint.position + Vector3.right * relativeX;
            var card = cards[i];

            if (newCards != null && i >= cards.Count - newCards.Count)
                card.Create(newCards[i - (cards.Count - newCards.Count)], pos);
            else
                card.MoveTo(pos);
        }
    }
}
