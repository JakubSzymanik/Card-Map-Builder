using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Runtime.InteropServices.WindowsRuntime;

public class GameController : MonoBehaviour
{
    [SerializeField] Map map;
    [SerializeField] HeroController hero;
    [SerializeField] Hand hand;
    [SerializeField] FieldMarkerHandler fieldMarkerHandler;

    BehaviorSubject<bool> canPlaceTile = new BehaviorSubject<bool>(true);

    void Start()
    {
        fieldMarkerHandler.OnMarkerClicked
            .Subscribe(v =>
            {
                canPlaceTile.OnNext(false);
                fieldMarkerHandler.HideFieldMarkers();
                map.PlaceTile(v, hand.TakeSelectedCard());
                hero.StartTurn(v, map.Tiles, map.Enemies);
            })
            .AddTo(fieldMarkerHandler);

        hero.OnTurnFinished
            .Subscribe(v => 
            {
                canPlaceTile.OnNext(true);
                hand.Draw(1);
            })
            .AddTo(hero);

        Observable.CombineLatest(canPlaceTile, hand.OnCardSelected, (canPlace, card) => canPlace && card != null)
            .Where(v => v)
            .Subscribe(v => fieldMarkerHandler.ShowFieldMarkers(hero.GridPosition, map.Tiles));
    }
}
