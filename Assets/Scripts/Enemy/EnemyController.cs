using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Patrol=0,
        Investigate=1,
        GettingHelp=3,
    }
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _trigger= 0.3f;
    [SerializeField] private PatrolRoute _patrolRoute;
    [SerializeField] private FieldOfView _fov;
    [SerializeField] public EnemyState _state = EnemyState.Patrol;
    [SerializeField] private float _WaitTime = 2f;

    [SerializeField] private EnemyController[] OtherRobots;

    private bool CheckingObject=false;
    private bool SeePartner=false;
    private bool IsOwner=false;
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
            else
            {
                if (_state == EnemyState.GettingHelp)
                {
                    UpdateGettingHel();
                }
            }
            
        }
    }

    

    public void InvestigatePoint(Vector3 investigatePoint)
    {
        _state = EnemyState.Investigate;
        _investigationPoint = investigatePoint;
        _agent.SetDestination(_investigationPoint);
    }

    public void CheckForHelp(Transform center)
    {
        if(OtherRobots[0]._state == EnemyState.GettingHelp || OtherRobots[0]._state == EnemyState.Investigate) return;
        _state = EnemyState.GettingHelp;
        _investigationPoint = center.position;
        _agent.SetDestination(_investigationPoint);
        _agent.speed = 0.1f;
        CheckingObject = true;
        IsOwner = true;
    }
    
    private void UpdateGettingHel()
    {
        timer += Time.deltaTime;
        if (CheckingObject)
        {
            Vector3 direction = (_investigationPoint - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
        }
        
        if (timer > _WaitTime && !SeePartner)
        {
            CheckingObject = false;
            _agent.SetDestination(OtherRobots[0].transform.position);
            _agent.speed = OtherRobots[0]._agent.speed + 2f;
            float DistanceToRobot = Vector3.Distance(transform.position, OtherRobots[0].transform.position);
            if (DistanceToRobot <= 0.5f)
            {
                SetRobotsToSeeEachOther();
            }
        }
        
        if (SeePartner)
        {
            Vector3 direction = (OtherRobots[0].transform.position-transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
        }

        if (timer > _WaitTime && IsOwner && SeePartner)
        {
            SetRobotsToSeeTheObjective();
        }
        
    }

    private void SetRobotsToSeeTheObjective()
    {
        _agent.speed = 2f;
        OtherRobots[0]._agent.speed = 2f;
        OtherRobots[0].SeePartner = false;
        IsOwner = false;
        SeePartner = false;
        timer = 0;
        InvestigatePoint(_investigationPoint);
        OtherRobots[0].InvestigatePoint(_investigationPoint);
    }

    private void SetRobotsToSeeEachOther()
    {
        SeePartner = true;
        OtherRobots[0].SeePartner = true;
        OtherRobots[0]._state = EnemyState.GettingHelp; 
        OtherRobots[0]._agent.SetDestination(_investigationPoint);
        OtherRobots[0]._agent.speed = 0.1f;
        _agent.speed = 0.1f;
        timer = 0;
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
