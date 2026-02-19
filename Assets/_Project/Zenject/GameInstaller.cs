using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Managers")]
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private EffectFactory effectFactory;
    [SerializeField] private InputManager inputManager;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolManager>().FromInstance(objectPoolManager).AsSingle();
        Container.Bind<EffectFactory>().FromInstance(effectFactory).AsSingle();
        Container.Bind<InputManager>().FromInstance(inputManager).AsSingle();
    }
}