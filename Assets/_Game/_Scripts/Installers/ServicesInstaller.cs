using UnityEngine;
using Zenject;

public class ServicesInstaller : MonoInstaller
{
    [SerializeField] private GameStateController _gameStateController;
    [SerializeField] private LevelsManager _levelsManager;
    [SerializeField] private InputHandler _inputHandler;

    public override void InstallBindings()
    {
        Container.Bind<GameStateController>().FromInstance(_gameStateController);
        Container.Bind<LevelsManager>().FromInstance(_levelsManager);
        Container.Bind<InputHandler>().FromInstance(_inputHandler);
    }
}