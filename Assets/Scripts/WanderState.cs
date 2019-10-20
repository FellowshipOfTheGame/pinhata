using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState {

    private Vector3? _destination;
    private float _stopDistance = 1f;
    private float _turnSpeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Wall");
    private float _rayDistance = 3.5f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private Enemy _enemy;

    public WanderState(Enemy enemy) : base(enemy.gameObject) {
        _enemy = enemy;
    }

    public override Type Tick() {
        var chaseTarget = CheckForAggro();
        if(chaseTarget != null) {
            _enemy.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }

        if(!_destination.HasValue || Vector3.Distance(transform.position, _destination.Value) <= _stopDistance)
              FindRandomDestination();

        transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, Time.deltaTime * _turnSpeed);

        if (IsForwardBlocked())
            transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, 0.2f);
        else
            transform.Translate(Vector3.forward * Time.deltaTime * EnemySettings.Speed);

        while (IsPathBlocked()) {
            FindRandomDestination();
            Debug.Log("Wall");
        }

        return null;
    }

    private bool IsForwardBlocked() {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }

    private bool IsPathBlocked() {
        Ray ray = new Ray(transform.position, _direction);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }

    private void FindRandomDestination() {
        Vector3 testPosition = transform.position + (transform.forward * 4f) 
            + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination.Value - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    private Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggro() {

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;

        for (var i = 0; i < 24; i++) {
            if (Physics.Raycast(pos, direction, out hit, EnemySettings.AggroRadius)) {

                var enemy = hit.collider.GetComponent<Player>();

                // Choose target
                if (enemy != null) {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return enemy.transform;
                }
                else {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else {
                Debug.DrawRay(pos, direction * EnemySettings.AggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
}
