using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] PlayerCharacterController player;
    [SerializeField] PlayerTurnHandler turnHandler;
    [SerializeField] Hand hand;
    [SerializeField] List<FieldMarker> fieldMarkers = new List<FieldMarker>();
    [SerializeField] List<Tile> tiles = new List<Tile>();
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    bool canPlaceTile = true;

    BehaviorSubject<bool> canPlaceTileSubject = new BehaviorSubject<bool>(true);
    //ISubject<bool> canPlaceTileSubject = new Subject<bool>();

    private void Start()
    {
        Observable.Merge(
            fieldMarkers[0].OnMouseDown, 
            fieldMarkers[1].OnMouseDown, 
            fieldMarkers[2].OnMouseDown, 
            fieldMarkers[3].OnMouseDown)
            .Subscribe(v => PlaceTile(v));

        Observable.CombineLatest(canPlaceTileSubject, hand.OnCardSelected, (canPlace, card) =>
        {
            return canPlace && card != null;
        }).Where(v => v)
        .Subscribe(v => SetFieldMarkers(player.GridPosition));
    }
    void PlaceTile(Vector2Int pos)
    {
        ClearFieldMarkers();
        Card card = hand.TakeCard();
        var obj = Instantiate(card.TilePrefab, GridPos.GridToWorldPos(pos), Quaternion.identity);
        Tile tile = obj.GetComponent<Tile>();
        tiles.Add(tile);

        canPlaceTileSubject.OnNext(false);
        StartTurn(pos);
    }
    void SetFieldMarkers(Vector2Int playerPos)
    {
        for(int i = 0; i < 4; i++)
        {
            Vector2Int dir = Direction.Directions[i];
            Vector2Int pos = Vector2Int.zero;
            bool empty = false;
            for(int j = 1; !empty; j++)
            {
                pos = playerPos + dir * j;
                empty = tiles.Where(tile => tile.GridPosition == pos).Count() <= 0;
            }
            fieldMarkers[i].transform.position = GridPos.GridToWorldPos(pos);
            fieldMarkers[i].Show(true);
        }
    }
    void ClearFieldMarkers()
    {
        foreach(var fieldMarker in fieldMarkers)
        {
            fieldMarker.Show(false);
        }
    }

    void StartTurn(Vector2Int target)
    {
        turnHandler.StartPlayerTurn(target, tiles, enemies, TurnCompleted);
        //player.MoveToTarget(target);
    }
    void TurnCompleted()
    {
        canPlaceTileSubject.OnNext(true);
        hand.Draw(1);
    }
}
