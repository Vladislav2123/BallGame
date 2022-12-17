using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Inject] private GameStateController _gameStateController;
    [Inject] private LevelsManager _levelsManager;

    [Inject] public InputHandler InputHandler { get; }
    public PlayerControllerStateMachine StateMachine { get; private set; }
    public Character Character => _levelsManager.Level.PlayerCharacter;

    private bool _canControlCharacter;
    private bool CanControlCharacter
    {
        get => _canControlCharacter;
        set
        {
            if (_canControlCharacter == value) return;

            _canControlCharacter = value;

            if(_canControlCharacter)
            {
                InputHandler.OnPressedEvent += StateMachine.SetStateAim;
                InputHandler.OnReleasedEvent += TryMoveCharacter;
                InputHandler.OnReleasedEvent += StateMachine.ResetState;
            }
            else
            {
                InputHandler.OnPressedEvent -= StateMachine.SetStateAim;
                InputHandler.OnReleasedEvent -= TryMoveCharacter;
                InputHandler.OnReleasedEvent -= StateMachine.ResetState;

                if (Character.IsAiming) StateMachine.ResetState();
            }
        }
    }

    private void Awake()
    {
        StateMachine = GetComponent<PlayerControllerStateMachine>();
    }

    private void OnEnable()
    {
        _gameStateController.OnGameStartedEvent += OnGameStated;
        _gameStateController.OnGameEndedEvent += OnGameEnded;
    }
    
    private void OnDisable()
    {
        _gameStateController.OnGameStartedEvent -= OnGameStated;
        _gameStateController.OnGameEndedEvent -= OnGameEnded;
    }


    private void Start()
    {
        CanControlCharacter = true;
    }

    private void OnGameStated()
    {
        CanControlCharacter = true;
    }

    private void OnGameEnded()
    {
        CanControlCharacter = false;
    }

    private void TryMoveCharacter()
    {
        if (Character.IsAiming) Character.TryMove();
    }
}