using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Transform Target { get; private set; }

    public StateMachine StateMachine => GetComponent<StateMachine>();

    private void Awake() {
        InitializeStateMachine();
    }

    private void InitializeStateMachine() {
        var states = new Dictionary<Type, BaseState>() {
            { typeof(WanderState), new WanderState(this) },
            { typeof(ChaseState), new ChaseState(this) },
            { typeof(AttackState), new AttackState(this) }
        };

        GetComponent<StateMachine>().SetStates(states);
        GetComponent<StateMachine>().SetIdleState(typeof(WanderState));
    }

    public void SetTarget(Transform target) {
        Target = target;
    }

    public virtual void Attack() {
        Debug.Log("Enemy Action");
    }
}
