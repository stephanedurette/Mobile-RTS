using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ObjectPoolManager objectPoolManager;
    [SerializeField] private SelectionManager selectionManager;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolManager>().FromInstance(objectPoolManager).AsSingle();
        Container.Bind<SelectionManager>().FromInstance(selectionManager).AsSingle();
    }
}