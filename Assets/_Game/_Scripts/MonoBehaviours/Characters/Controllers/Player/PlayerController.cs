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
        _gameStateController.OnGameStartedEvent += OnGameStarted;
        _gameStateController.OnGameEndedEvent += OnGameEnded;
        Character.OnCollisionEvent += LoseGame;
    }
    
    private void OnDisable()
    {
        _gameStateController.OnGameStartedEvent -= OnGameStarted;
        _gameStateController.OnGameEndedEvent -= OnGameEnded;
        Character.OnCollisionEvent -= LoseGame;
    }


    private void OnGameStarted()
    {
        CanControlCharacter = true;
        Debug.Log("GameStarted");
    }

    private void OnGameEnded()
    {
        CanControlCharacter = false;
    }

    private void TryMoveCharacter()
    {
        if (Character.IsAiming) Character.TryMove();
    }

    private void LoseGame()
    {
        _gameStateController.State = GameState.Lose;
    }
}