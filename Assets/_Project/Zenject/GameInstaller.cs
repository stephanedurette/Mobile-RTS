using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Managers")]
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GridManager gridManager;

    [Header("Factories")]
    [SerializeField] private EffectFactory effectFactory;

    [Header("Players")]
    [SerializeField] private Player player;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolManager>().FromInstance(objectPoolManager).AsSingle();
        Container.Bind<EffectFactory>().FromInstance(effectFactory).AsSingle();
        Container.Bind<InputManager>().FromInstance(inputManager).AsSingle();
        Container.Bind<GridManager>().FromInstance(gridManager).AsSingle();
        Container.Bind<Player>().FromInstance(player).AsSingle();
    }
}