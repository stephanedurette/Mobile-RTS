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

    public bool IsComponentOnGrid<T>(Vector2Int gridOriginPosition, int checkWidth, int checkHeight, out T component) { 
        component = default(T);
        return false;
    }

    public void HighlightSquares(Vector2Int gridOriginPosition, int width, int height, Color highlightColor)
    {

    }

    public void ClearHighlights()
    {

    }
}
