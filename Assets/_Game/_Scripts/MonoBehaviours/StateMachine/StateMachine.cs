using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StateMachine : MonoBehaviour
{
    protected Dictionary<Type, IState> _states;
    private IState _currentState;

    protected virtual void Awake()
    {
        _states = new Dictionary<Type, IState>();
        InitializeStates();
    }

    private void Update()
    {
        if (_currentState != null) _currentState.Update();
    }

    protected abstract void InitializeStates();

    protected void SetState<T>() where T : IState
    {
        IState state = GetState<T>();

        if(state == null)
        {
            Debug.LogError($"[StateMachine] {gameObject.name} State Machine has no setting state");
            return;
        }

        if (_currentState != null) _currentState.Exit();

        _currentState = state;
        _currentState.Enter();
    }

    public void ResetState()
    {
        if (_currentState != null) _currentState.Exit();
        _currentState = null;
    }

    protected IState GetState<T>() where T : IState
    {
        var type = typeof(T);
        return _states[type];
    }
}