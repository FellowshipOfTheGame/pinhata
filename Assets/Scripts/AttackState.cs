using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState {

    private float _attackReadyTimer;
    private Enemy _enemy;

    public AttackState(Enemy enemy) : base(enemy.gameObject) {
        _enemy = enemy;
    }

    public override Type Tick() {
        if (_enemy.Target == null)
            return typeof(WanderState);

        _attackReadyTimer -= Time.deltaTime;

        if(_attackReadyTimer <= 0f) {
            _enemy.Attack();
            Debug.Log("Attack");
        }

        return null;
    }
}
