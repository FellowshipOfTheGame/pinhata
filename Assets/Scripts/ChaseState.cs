using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState {

    private Enemy _enemy;

    public ChaseState(Enemy enemy) : base(enemy.gameObject) {
        _enemy = enemy;
    }

    public override Type Tick() {
        if (_enemy.Target == null)
            return typeof(WanderState);

        transform.LookAt(_enemy.Target);
        transform.Translate(Vector3.forward * Time.deltaTime * EnemySettings.Speed);

        var distance = Vector3.Distance(transform.position, _enemy.Target.transform.position);
        if (distance <= EnemySettings.AttackRange)
            return typeof(AttackState);

        return null;
    }
}
