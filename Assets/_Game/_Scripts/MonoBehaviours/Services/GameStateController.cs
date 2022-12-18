using UnityEngine;
using System;
using Zenject;

public enum GameState
{
    Menu, Playing, ReviveCooldown, Win, Lose
}

public class GameStateController : MonoBehaviour
{
    public event Action<GameState> OnStateChangedEvent;
    public event Action OnMenuStateEvent;
    public event Action OnGameStartedEvent;
    public event Action OnGameEndedEvent;
    public event Action OnWinEvent;
    public event Action OnLoseEvent;

    [SerializeField] private GameState _state;
    [SerializeField] private float _resultShowingDelay;

    [Inject] private LevelDisplay _levelDisplay;
    [Inject] private WinWindow _winWindow;
    [Inject] private LoseWindow _loseWindow;

    public bool IsPlaying { get; private set; }
    public GameState State
    {
        get => _state;
        set
        {
            _state = value;

            switch(value)
            {
                case GameState.Menu:
                    SetMenuState();
                    break;

                case GameState.Playing:
                    TryStartGame();
                    break;
                
                case GameState.ReviveCooldown:
                    StartReviveCooldown();
                    break;

                case GameState.Win:
                    TryWin();
                    break;

                case GameState.Lose:
                    TryLose();
                    break;
            }

            OnStateChangedEvent?.Invoke(_state);
        }
    }


    private void Start()
    {
        State = _state;
    }

    private void SetMenuState()
    {
        IsPlaying = false;
        OnMenuStateEvent?.Invoke();
    }

    private void TryStartGame()
    {
        if (IsPlaying) return;

        IsPlaying = true;
        OnGameStartedEvent?.Invoke();
    }

    private void StartReviveCooldown()
    {

    }

    private void TryWin()
    {
        if (IsPlaying == false) return;
        EndGame();

        _winWindow.Show(_resultShowingDelay);

        OnWinEvent?.Invoke();
    }


    private void TryLose()
    {
        if (IsPlaying == false) return;
        EndGame();

        _loseWindow.Show(_resultShowingDelay);

        OnLoseEvent?.Invoke();
    }

    private void EndGame()
    {
        IsPlaying = false;

        _levelDisplay.TryHide();

        OnGameEndedEvent?.Invoke();
    }
}