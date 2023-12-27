using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] HeroMover mover;
    [SerializeField] HeroStatsHandler statHandler;

    public IObservable<bool> OnTurnFinished { get { return OnTurnFinishedSubject; } }
    Subject<bool> OnTurnFinishedSubject = new Subject<bool> ();

    public Vector2Int GridPosition { get { return GridUtility.WorldToGridPos(transform.position); } }

    List<Tile> tiles = new List<Tile>();
    List<Enemy> enemies = new List<Enemy>();
    Vector2Int currentDirection = Vector2Int.zero;
    Vector2Int? currentTarget = null;


    private void Start()
    {
        mover.OnActionFinished.Where(_ => currentTarget != null)
            .Subscribe(_ => StartCoroutine(ActionFinished()))
            .AddTo(this);
    }
    public void StartTurn(Vector2Int target, List<Tile> tiles, List<Enemy> enemies)
    {
        this.tiles = tiles;
        this.enemies = enemies;
        currentTarget = target;
        currentDirection = GridUtility.GetDirection(GridPosition, target);
        Action();
    }
    void EndTurn()
    {
        tiles = null;
        enemies = null;
        currentTarget = null;
        currentDirection = Vector2Int.zero;
        statHandler.EndTurn();
        OnTurnFinishedSubject.OnNext(true);
    }
    void Action()
    {
        //if (GridPosition == currentTarget) { EndTurn(); return; }

        Enemy nextEnemy = GetEnemy(GridPosition + currentDirection);
        if (nextEnemy == null)
            mover.Move(GridPosition + currentDirection);
        else
        {
            mover.Fight(nextEnemy);
            return;
        }
    }
    IEnumerator ActionFinished()
    {
        Tile tile = GetTile(GridPosition);
        Card card = tile.Activate();
        if(card != null && card.StatEffects != null && card.StatEffects.Count > 0)
            statHandler.AddStatEffects(tile.card.StatEffects);

        if (GridPosition == currentTarget) { EndTurn(); yield break ; }

        yield return new WaitForSeconds(0.5f);
        Action();
    }

    Enemy GetEnemy(Vector2Int pos)
    {
        return enemies.Where(e => e.GridPosition == pos).FirstOrDefault();
    }
    Tile GetTile(Vector2Int pos)
    {
        return tiles.Where(t => t.GridPosition == pos).FirstOrDefault();
    }
}
