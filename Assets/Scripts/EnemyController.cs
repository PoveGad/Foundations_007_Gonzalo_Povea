using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float trigger= 0.3f;
    [SerializeField] private Transform[] point;
    

    private Transform currentPoint  ;

    private int index = 0;

    private bool moving = false;

    private void Update()
    {
        if (!moving)
        {
            if (index >= point.Length)
            {
                index = 0;
            }

            currentPoint = point[index];
            
            _agent.SetDestination(currentPoint.position);
            moving = true;
            
        }

        if (moving && Vector3.Distance(transform.position,currentPoint.position) < trigger)
        {
            moving = false;
            index+=1;
        }
        
    }
}
