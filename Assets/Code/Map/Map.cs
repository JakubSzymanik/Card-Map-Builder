using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Map : MonoBehaviour
{
    //[SerializeField] PlayerCharacterController player;
    //[SerializeField] PlayerTurnHandler turnHandler;
    //[SerializeField] Hand hand;
    //[SerializeField] List<FieldMarker> fieldMarkers = new List<FieldMarker>();
    [SerializeField] List<Tile> tiles = new List<Tile>();
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    //bool canPlaceTile = true;
    public List<Tile> Tiles { get { return tiles; } }
    public List<Enemy> Enemies { get {  return enemies; } }
    //BehaviorSubject<bool> canPlaceTileSubject = new BehaviorSubject<bool>(true);
    //ISubject<bool> canPlaceTileSubject = new Subject<bool>();

    //private void Start()
    //{
    //    Observable.Merge(
    //        fieldMarkers[0].OnMouseDown,
    //        fieldMarkers[1].OnMouseDown,
    //        fieldMarkers[2].OnMouseDown,
    //        fieldMarkers[3].OnMouseDown)
    //        .Subscribe(v => PlaceTile(v));

    //    Observable.CombineLatest(canPlaceTileSubject, hand.OnCardSelected, (canPlace, card) =>
    //    {
    //        return canPlace && card != null;
    //    }).Where(v => v)
    //    .Subscribe(v => SetFieldMarkers(player.GridPosition));
    //}
    public void PlaceTile(Vector2Int pos, Card card)
    {
        //ClearFieldMarkers();
        //Card card = hand.TakeCard();
        var obj = Instantiate(card.TilePrefab, GridUtility.GridToWorldPos(pos), Quaternion.identity);
        Tile placedTile = obj.GetComponent<Tile>();
        placedTile.Create(card);
        tiles.Add(placedTile);

        //canPlaceTileSubject.OnNext(false);
        //StartTurn(pos);
    }
    public void PlaceEnemy(Vector2Int pos, Enemy enemy)
    {
        var obj = Instantiate(enemy, GridUtility.GridToWorldPos(pos), Quaternion.identity);
        //Tile placedEnemy = obj.GetComponent<Tile>();
        enemies.Add(obj);
    }

    //void StartTurn(Vector2Int target)
    //{
    //    turnHandler.StartPlayerTurn(target, tiles, enemies, TurnCompleted);
    //    //player.MoveToTarget(target);
    //}
    //void TurnCompleted()
    //{
    //    canPlaceTileSubject.OnNext(true);
    //    hand.Draw(1);
    //}
}
