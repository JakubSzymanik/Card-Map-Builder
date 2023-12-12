using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Unity.VisualScripting;

public class Hand : MonoBehaviour
{
    [SerializeField] Deck deck;
    [SerializeField] CardPlacer cardPlacer;
    [SerializeField] CardInHand cardInHandPrefab;
    List<CardInHand> cardsInHand = new List<CardInHand>();
    CardInHand selectedCard;
    public IObservable<Card> OnCardSelected { get { return cardSubject; } }
    ISubject<Card> cardSubject = new Subject<Card>();


    private void Start()
    {
        Draw(3);
    }

    public Card TakeCard()
    {
        var card = new Card(selectedCard.Card);
        cardsInHand.Remove(selectedCard);
        selectedCard.Destroy();
        selectedCard = null;
        return card;
    }

    public void Draw(int amount)
    {
        List<Card> cards = new List<Card>();
        for(int i = 0 ; i < amount; i++)
        {
            var cardData = deck.Draw();
            cards.Add(cardData);

            var spawnedCardInHand = Instantiate(cardInHandPrefab);
            spawnedCardInHand.transform.parent = transform;

            spawnedCardInHand.OnMouseDown.Subscribe(v =>
            {
                selectedCard?.Highlight(false);
                selectedCard = v;
                selectedCard.Highlight(true);
                cardSubject.OnNext(selectedCard.Card);
            }).AddTo(spawnedCardInHand);


            cardsInHand.Add(spawnedCardInHand);
            cardPlacer.PlaceCards(cardsInHand, cards);
        }
    }
}
