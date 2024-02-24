using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float trigger= 0.5f;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    private Transform currentPoint  ;

    private bool moving = false;

    private void Update()
    {
        if (!moving)
        {
            
            if (currentPoint==point1)
            {
                currentPoint = point2;
            }
            else
            {
                currentPoint = point1;
            }
            _agent.SetDestination(currentPoint.position);
            moving = true;
            
        }

        if (moving && Vector3.Distance(transform.position,currentPoint.position) < trigger)
        {
            moving = false;
        }3
        
    }
}
