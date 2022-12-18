using UnityEngine;
using Zenject;

public class FXInstaller : MonoInstaller
{
    [SerializeField] private CameraShake _caneraShake;

    public override void InstallBindings()
    {
        Container.Bind<CameraShake>().FromInstance(_caneraShake);
    }
}