using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2Int GridPosition { get { return GridUtility.WorldToGridPos(transform.position); } }
}
