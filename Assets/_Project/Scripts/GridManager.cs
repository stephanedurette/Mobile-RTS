using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float gridSquareSize = 1f;

    public Vector2 WorldPositionSnappedToGrid(Vector2 worldPosition) => WorldPosition(GridPosition(worldPosition));

    public Vector2Int GridPosition(Vector2 worldPosition)
    {
        return new Vector2Int(
                Mathf.RoundToInt(worldPosition.x / gridSquareSize),
                Mathf.RoundToInt(worldPosition.y / gridSquareSize)
            );
    }

    public Vector2 WorldPosition(Vector2Int gridPosition)
    {
        return (Vector2)gridPosition * gridSquareSize;
    }

    public bool IsComponentOnGrid<T>(Vector2 gridWorldPosition, Vector2 gridDimensions, out T component) { 
        var hits = Physics2D.OverlapBoxAll(gridWorldPosition, gridSquareSize * (gridDimensions - Vector2.one), 0f);
        
        foreach (var hit in hits) { 
            if (hit.gameObject.TryGetComponent(out T t))
            {
                component = t;
                return true;
            }
        }

        component = default(T);
        return false;
    }

    public void HighlightSquares(Vector2 gridWorldPosition, int width, int height, Color highlightColor)
    {

    }

    public void ClearHighlights()
    {

    }
}
