using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Managers")]
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GridManager gridManager;

    [Header("Factories")]
    [SerializeField] private SpawnFactory spawnFactory;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolManager>().FromInstance(objectPoolManager).AsSingle();
        Container.Bind<SpawnFactory>().FromInstance(spawnFactory).AsSingle();
        Container.Bind<InputManager>().FromInstance(inputManager).AsSingle();
        Container.Bind<GridManager>().FromInstance(gridManager).AsSingle();
    }
}