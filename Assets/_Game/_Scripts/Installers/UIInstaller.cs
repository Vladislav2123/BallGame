using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private UIFade _uiFade;
    [SerializeField] private LevelDisplay _levelDisplay;
    [SerializeField] private WinWindow _winWindow;
    [SerializeField] private LoseWindow _loseWindow;

    public override void InstallBindings()
    {
        Container.Bind<Canvas>().FromInstance(_canvas);
        Container.Bind<UIFade>().FromInstance(_uiFade);
        Container.Bind<LevelDisplay>().FromInstance(_levelDisplay);
        Container.Bind<WinWindow>().FromInstance(_winWindow);
        Container.Bind<LoseWindow>().FromInstance(_loseWindow);
    }
}