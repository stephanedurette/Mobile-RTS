using UnityEngine;
using Zenject;

public class EffectFactory : MonoBehaviour
{
    [SerializeField] private GameObject moveEffectPrefab;
    [SerializeField] private GameObject placeEffectPrefab;

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
}
