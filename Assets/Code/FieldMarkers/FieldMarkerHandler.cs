using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;

public class FieldMarkerHandler : MonoBehaviour
{
    [SerializeField] List<FieldMarker> fieldMarkers = new List<FieldMarker>();
    
    public IObservable<Vector2Int> OnMarkerClicked { get
        { return Observable.Merge(
            fieldMarkers[0].OnMouseDown,
            fieldMarkers[1].OnMouseDown,
            fieldMarkers[2].OnMouseDown,
            fieldMarkers[3].OnMouseDown);
        }}

    private void Start()
    {
        //OnMarkerClicked = Observable.Merge(
        //    fieldMarkers[0].OnMouseDown, 
        //    fieldMarkers[1].OnMouseDown,
        //    fieldMarkers[2].OnMouseDown,
        //    fieldMarkers[3].OnMouseDown);
    }

    public void ShowFieldMarkers(Vector2Int playerPos, List<Tile> tiles)
    {
        for (int i = 0; i < 4; i++)
        {
            Vector2Int dir = Direction.Directions[i];
            Vector2Int pos = Vector2Int.zero;
            bool empty = false;
            for (int j = 1; !empty; j++)
            {
                pos = playerPos + dir * j;
                empty = tiles.Where(tile => tile.GridPosition == pos).Count() <= 0;
            }
            fieldMarkers[i].transform.position = GridUtility.GridToWorldPos(pos);
            fieldMarkers[i].Show(true);
        }
    }
    public void HideFieldMarkers()
    {
        foreach (var fieldMarker in fieldMarkers)
        {
            fieldMarker.Show(false);
        }
    }
}
