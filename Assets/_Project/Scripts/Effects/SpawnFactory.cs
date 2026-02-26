using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SpawnFactory : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private GameObject moveEffectPrefab;
    [SerializeField] private GameObject placeEffectPrefab;

    [Header("Grid Highlights")]
    [SerializeField] private GameObject gridSquareHighlightPrefab;

    [Header("UI Elements")]
    [SerializeField] private GameObject confirmationWindowPrefab;

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

    public ConfirmationWindow CreateConfirmationWindow(Vector2 position, UnityAction onConfirm, UnityAction onCancel) {
        var obj = objectPoolManager.SpawnObject<ConfirmationWindow>(confirmationWindowPrefab, position);
        obj.Bind(onConfirm, onCancel);
        return obj;
    }
}
