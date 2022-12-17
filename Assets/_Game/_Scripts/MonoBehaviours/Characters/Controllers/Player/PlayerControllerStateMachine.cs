using UnityEngine;

public class PlayerControllerStateMachine : StateMachine
{
    private PlayerController _controller;

    protected override void Awake()
    {
        _controller = GetComponent<PlayerController>();

        base.Awake();
    }

    protected override void InitializeStates()
    {
        _states[typeof(PlayerControllerStateAim)] = new PlayerControllerStateAim(_controller);
    }

    public void SetStateAim()
    {
        SetState<PlayerControllerStateAim>();
    }
}
