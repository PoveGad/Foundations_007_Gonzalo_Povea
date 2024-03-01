using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    enum EnemyState
    {
        Patrol=0,
        Investigate=1,
    }
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _trigger= 0.3f;
    [SerializeField] private PatrolRoute _patrolRoute;
    [SerializeField] private FieldOfView _fov;
    [SerializeField] private EnemyState _state = EnemyState.Patrol;
    [SerializeField] private float _WaitTime = 2f;
    
    private Transform currentPoint  ;
    private bool moving = false;
    private int _routeIndex = 0;
    private bool _forwardsAlongPath = true;
    private Vector3 _investigationPoint;
    private float timer;

    private void Start()
    {
        currentPoint = _patrolRoute.route[_routeIndex];
    }
    private void Update()
    {
        if (_fov.visibleObjects.Count > 0)
        {
            InvestigatePoint(_fov.visibleObjects[0].position);
        }
        if (_state == EnemyState.Patrol)
        {
            UpdatePatrol();
        }
        else
        {
            if (_state == EnemyState.Investigate)
            {
                UpdateInvestigate();
            }
        }
    }

    public void InvestigatePoint(Vector3 investigatePoint)
    {
        _state = EnemyState.Investigate;
        _investigationPoint = investigatePoint;
        _agent.SetDestination(_investigationPoint);
    }

    private void UpdateInvestigate()
    {
        if (Vector3.Distance(transform.position,_investigationPoint) < _trigger)
        {
            timer += Time.deltaTime;
            if (timer > _WaitTime)
            {
                ReturnToPatrol();
            }
        }
    }

    private void ReturnToPatrol()
    {
        _state = EnemyState.Patrol;
        timer = 0f;
        moving = false;
    }

    private void UpdatePatrol()
    {
        if (!moving)
        {
            NextPatrolPoint();
            _agent.SetDestination(currentPoint.position);
            moving = true;
            
        }

        if (moving && Vector3.Distance(transform.position,currentPoint.position) < _trigger)
        {
            moving = false;
        }
    }

    private void NextPatrolPoint()
    {
        if (_forwardsAlongPath)
        {
            _routeIndex++;
        }
        else
        {
            _routeIndex--;
        }

        if (_routeIndex == _patrolRoute.route.Count)
        {
            if(_patrolRoute.patrolType == PatrolRoute.PatrolType.Loop){ _routeIndex = 0;}
            else
            {
                _forwardsAlongPath = false;
                _routeIndex-=2;
            }
        }

        if (_routeIndex == 0)
        {
            _forwardsAlongPath = true;
        }
        currentPoint = _patrolRoute.route[_routeIndex];
    }
}
