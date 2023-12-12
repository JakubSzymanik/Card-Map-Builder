using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerTurnHandler : MonoBehaviour
{
    [SerializeField] PlayerCharacterController player;
    List<Tile> tiles = new List<Tile>();
    List<Enemy> enemies = new List<Enemy>();

    Vector2Int currentDirection = Vector2Int.zero;
    Vector2Int currentTarget = Vector2Int.zero;
    Action turnCompletedCallback;

    public void StartPlayerTurn(Vector2Int target, List<Tile> tiles, List<Enemy> enemies, Action turnCompletedCallback)
    {
        this.turnCompletedCallback = turnCompletedCallback;
        this.currentTarget = target;
        this.tiles = tiles;
        this.enemies = enemies;
        Vector3 target3D = GridPos.GridToWorldPos(target);
        Vector2 direction = (target3D - transform.position).normalized; //float direction
        currentDirection = Vector2Int.RoundToInt(direction);
        Action();
    }

    void Action()
    {
        var tile = GetTile(player.GridPosition + currentDirection);
        var enemy = GetEnemy(player.GridPosition + currentDirection);
        player.Action(tile, enemy, ActionFinished);
    }
    void ActionFinished()
    {
        if(player.GridPosition ==  currentTarget)
        {
            TurnFinished();
            return;
        }

        Action();
    }
    void TurnFinished()
    {
        turnCompletedCallback();

        //cleanup
        currentTarget = Vector2Int.zero;
        currentDirection = Vector2Int.zero;
        turnCompletedCallback = null;
    }

    Enemy GetEnemy(Vector2Int pos)
    {
        return null;
    }
    Tile GetTile(Vector2Int pos)
    {
        return tiles.Where(t => t.GridPosition == pos).FirstOrDefault();
    }
}
