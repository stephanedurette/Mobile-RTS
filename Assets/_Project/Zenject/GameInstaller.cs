using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Managers")]
    [SerializeField] private ObjectPoolManager objectPoolManager;

    [Header("Effects")]
    [SerializeField] private GameObject placementCursorPrefab;
    [SerializeField] private GameObject moveCursorPrefab;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolManager>().FromInstance(objectPoolManager).AsSingle();

        Container.BindInstance(placementCursorPrefab).WithId("PlacementCursor");
        Container.BindInstance(moveCursorPrefab).WithId("MoveCursor");
    }
}