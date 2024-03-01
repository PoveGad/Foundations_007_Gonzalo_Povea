using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
   [SerializeField] private Color _giszmoColor = Color.red;
   [SerializeField] private float _viewRadius = 6f;
   [SerializeField] private float _viewAngle = 30f;
   [SerializeField] private Creature _creature;
   [SerializeField] private LayerMask _BlockingLayers;
   public List<Transform> visibleObjects;

   private void Update()
   {
      visibleObjects.Clear();
      Collider[] targetsInViewrRadius = Physics.OverlapSphere(transform.position, _viewRadius);
      foreach (Collider target in targetsInViewrRadius)
      {
         if(!target.TryGetComponent(out Creature targetCreature)) continue;
         if(_creature.team == targetCreature.team)continue;
         
         Vector3 directionTarget = (target.transform.position - transform.position).normalized;

         if (Vector3.Angle(transform.forward, directionTarget) < _viewAngle)
         {
            Vector3 headPos = _creature.head.position;
            Vector3 targetHeadPos = targetCreature.head.position;
            Vector3 dirToTargetHead = (targetHeadPos - headPos).normalized;
            float disToTargetHead = Vector3.Distance(headPos, targetHeadPos);
            if(Physics.Raycast(headPos, dirToTargetHead, disToTargetHead, _BlockingLayers)){continue;}
            Debug.DrawLine(headPos,targetHeadPos,Color.blue);
            visibleObjects.Add(target.transform);
            
            
         }
      }
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = _giszmoColor;
      Handles.color = _giszmoColor;
      

      Handles.DrawWireArc(transform.position, transform.up, transform.forward, _viewAngle, _viewRadius);
      Handles.DrawWireArc(transform.position, transform.up, transform.forward, -_viewAngle, _viewRadius);

      Vector3 lineA = Quaternion.AngleAxis(_viewAngle, transform.up) * transform.forward;
      Vector3 lineB = Quaternion.AngleAxis(-_viewAngle, transform.up) * transform.forward;
      Handles.DrawLine(transform.position, (lineA * _viewRadius) + transform.position);
      Handles.DrawLine(transform.position, (lineB * _viewRadius) + transform.position);
   }
}
