using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    private Dictionary<Type, BaseState> _avaliableStates;
    private Type _idleState;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states) {
        _avaliableStates = states;
    }

    public void SetIdleState(Type type) {
        _idleState = type;
    }

    private void Update() {
        if (CurrentState == null)
            CurrentState = _avaliableStates[_idleState];

        var nextState = CurrentState?.Tick();

        if(nextState != null && nextState != CurrentState?.GetType()) {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState) {
        CurrentState = _avaliableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
}
