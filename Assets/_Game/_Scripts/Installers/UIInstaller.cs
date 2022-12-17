using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private UIFade _uiFade;

    public override void InstallBindings()
    {
        Container.Bind<Canvas>().FromInstance(_canvas);
        Container.Bind<UIFade>().FromInstance(_uiFade);
    }
}