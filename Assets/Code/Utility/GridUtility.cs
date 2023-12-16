using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GridUtility
{
    public static Vector2Int WorldToGridPos(Vector2 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x);
        int y = Mathf.FloorToInt(worldPos.y);
        return new Vector2Int(x, y);
    }
    public static Vector2Int WorldToGridPos(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x);
        int y = Mathf.FloorToInt(worldPos.y);
        return new Vector2Int(x, y);
    }

    public static Vector3 GridToWorldPos(Vector2Int gridPos)
    {
        float x = gridPos.x + 0.5f;
        float y = gridPos.y + 0.5f;
        return new Vector3(x, y, 0);
    }

    public static Vector2Int GetDirection(Vector2Int from, Vector2Int to)
    {
        Vector3 target3D = GridToWorldPos(to);
        Vector2 direction = (target3D - GridToWorldPos(from)).normalized; //float direction
        return Vector2Int.RoundToInt(direction);
    }
}
