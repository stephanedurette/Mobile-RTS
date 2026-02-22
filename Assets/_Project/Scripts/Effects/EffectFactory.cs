using UnityEngine;
using Zenject;

public class EffectFactory : MonoBehaviour
{
    [SerializeField] private GameObject moveEffectPrefab;
    [SerializeField] private GameObject placeEffectPrefab;
    [SerializeField] private GameObject gridSquareHighlightPrefab;

    private ObjectPoolManager objectPoolManager;

    [Inject]
    public void Construct(ObjectPoolManager objectPoolManager)
    {
        this.objectPoolManager = objectPoolManager;
    }

    public PlacementCursor CreatePlacementCursor(Vector2 position)
    {
        return objectPoolManager.SpawnObject<PlacementCursor>(placeEffectPrefab, position);
    }

    public SelectionCursor CreateSelectionCursor(Vector2 position)
    {
        return objectPoolManager.SpawnObject<SelectionCursor>(moveEffectPrefab, position);
    }

    public GridSquareHighlight CreateGridSquareHighlight(Vector2 position, Color color) 
    {
        var obj = objectPoolManager.SpawnObject<GridSquareHighlight>(gridSquareHighlightPrefab, position);
        obj.HighlightColor = color;
        return obj;
    }
}
