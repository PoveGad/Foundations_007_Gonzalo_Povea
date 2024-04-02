using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneController : MonoBehaviour
{
  [SerializeField]private NavMeshAgent _agent;
  [SerializeField] private float _trigger= 0.3f;
  private bool moving = false;
  private Transform currentPoint;
  private int _routeIndex = 0;
  [SerializeField] private PatrolRoute _patrolRoute;
  private bool _forwardsAlongPath = true;
  private void Start()
  {
    currentPoint = _patrolRoute.route[_routeIndex];
  }


  private void Update(){
    UpdatePatrol();
  }

  private void UpdatePatrol()
  {
    if (!moving)
    {
      NextPatrolPoint();
      _agent.SetDestination(currentPoint.position);
      moving = true;

    }
    Debug.Log(Vector3.Distance(transform.position, currentPoint.position));

    if (moving && Vector3.Distance(transform.position, currentPoint.position) < _trigger)
    {
      moving = false;
    }

  }
  private void NextPatrolPoint()
  {
    Debug.Log(_routeIndex);
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
