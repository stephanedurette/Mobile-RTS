using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float gridSquareSize = 1f;

    public Vector2 WorldPositionSnappedToGrid(Vector2 worldPosition) => WorldPosition(GridPosition(worldPosition));

    private EffectFactory effectFactory;

    private HashSet<GridSquareHighlight> activeHighlights;

    private void Awake()
    {
        activeHighlights = new HashSet<GridSquareHighlight>();
    }

    [Inject]
    public void Construct(EffectFactory effectFactory)
    {
        this.effectFactory = effectFactory;
    }

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

    public bool IsComponentOnEveryGridPosition<T>(Vector2 worldPosition, Vector2Int gridDimensions)
    {
        Vector2 offset = new(
            gridDimensions.x % 2 == 0 ? gridSquareSize / 2 + gridDimensions.x / 2 : Mathf.Floor(gridDimensions.x / 2),
            gridDimensions.y % 2 == 0 ? gridSquareSize / 2 + gridDimensions.y / 2 : Mathf.Floor(gridDimensions.y / 2)
        );

        for (int x = 0; x < gridDimensions.x; x++)
        {
            for (int y = 0; y < gridDimensions.y; y++)
            {
                var hits = Physics2D.OverlapBoxAll(worldPosition - offset + new Vector2(x, y), gridSquareSize * .9f * Vector2.one, 0f);
                foreach (var hit in hits)
                {
                    if (!hit.gameObject.TryGetComponent(out T _))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public bool IsComponentOnGrid<T>(Vector2 gridWorldPosition, Vector2Int gridDimensions, out T component) { 
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

    public void HighlightSquares(Vector2 gridWorldPosition, Vector2Int gridDimensions, Color highlightColor)
    {
        Vector2 offset = new(
            gridDimensions.x % 2 == 0 ? gridSquareSize / 2 + gridDimensions.x / 2 : Mathf.Floor(gridDimensions.x / 2),
            gridDimensions.y % 2 == 0 ? gridSquareSize / 2 + gridDimensions.y / 2 : Mathf.Floor(gridDimensions.y / 2)
        );

        for (int x = 0; x < gridDimensions.x; x++) {
            for (int y = 0; y < gridDimensions.y; y++) {
                var highlight = effectFactory.CreateGridSquareHighlight(gridWorldPosition - offset/2 + new Vector2(x, y), highlightColor);
                activeHighlights.Add(highlight);
            }
        }
    }

    public void ClearHighlights()
    {
        foreach (var highlight in activeHighlights) { 
            highlight.gameObject.SetActive(false);
        }
        activeHighlights.Clear();
    }
}
