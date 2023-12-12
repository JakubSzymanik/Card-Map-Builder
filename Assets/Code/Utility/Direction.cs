using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    public static List<Vector2Int> Directions 
    { get { return new List<Vector2Int>() { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left}; } }
}
