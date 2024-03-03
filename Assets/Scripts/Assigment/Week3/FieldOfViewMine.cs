using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor;


public class FieldOfViewMine : MonoBehaviour
{
   [SerializeField] private Color _giszmoColor = Color.red;
   [SerializeField] private float _viewRadius = 1f;
   [SerializeField] private float _viewAngle = 30f;
   [SerializeField] private Creature _creature;
   [SerializeField] private LayerMask _BlockingLayers;
   [SerializeField] private float DistanceToExplote = 1.5f;
   [SerializeField] private Rigidbody[] Rigidbodies;
   public List<Transform> visibleObjects;

   [SerializeField] private ParticleSystem ExploteAnimation;

   private void Update()
   {
      visibleObjects.Clear();
      Collider[] targetsInViewrRadius = Physics.OverlapSphere(transform.position, _viewRadius);
      foreach (Collider target in targetsInViewrRadius)
      {
         if(!target.TryGetComponent(out Creature targetCreature)) continue;
         if (targetCreature.team == Creature.Team.Enemy)
         {

            Vector3 directionTarget = (target.transform.position - transform.position).normalized;

            if (Vector3.Angle(transform.up, directionTarget) < _viewAngle)
            {
               Vector3 headPos = _creature.head.position;
               Vector3 targetHeadPos = targetCreature.head.position;
               Vector3 dirToTargetHead = (targetHeadPos - headPos).normalized;
               float disToTargetHead = Vector3.Distance(headPos, targetHeadPos);
               if (Physics.Raycast(headPos, dirToTargetHead, disToTargetHead, _BlockingLayers))
               {
                  if (disToTargetHead <= DistanceToExplote)
                  {
                     ExploteRobot(target.gameObject);
                  }
               }

               
               visibleObjects.Add(target.transform);


            }
         }
      }
   }

   private void ExploteRobot(GameObject robot)
   {
      Instantiate(ExploteAnimation, robot.transform.position, quaternion.identity);
      robot.GetComponent<EnemyController>().enabled = false;
      foreach (Rigidbody bodypart in Rigidbodies)
      {
         bodypart.isKinematic = false;
      }
      gameObject.SetActive(false);
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = _giszmoColor;
      Handles.color = _giszmoColor;
      

      Handles.DrawWireArc(transform.position, transform.forward, transform.up, _viewAngle, _viewRadius);
      Handles.DrawWireArc(transform.position, transform.forward, transform.up, -_viewAngle, _viewRadius);
      
      Handles.DrawWireArc(transform.position, transform.right, transform.up, _viewAngle, _viewRadius);
      Handles.DrawWireArc(transform.position, transform.right, transform.up, -_viewAngle, _viewRadius);

      Vector3 lineA = Quaternion.AngleAxis(_viewAngle, transform.forward) * transform.up;
      Vector3 lineB = Quaternion.AngleAxis(-_viewAngle, transform.forward) * transform.up;
      Vector3 lineC = Quaternion.AngleAxis(_viewAngle, transform.right) * transform.up;
      Vector3 lineD = Quaternion.AngleAxis(-_viewAngle, transform.right) * transform.up;
      
      Handles.DrawLine(transform.position, (lineA * _viewRadius) + transform.position);
      Handles.DrawLine(transform.position, (lineB * _viewRadius) + transform.position);
      Handles.DrawLine(transform.position, (lineC * _viewRadius) + transform.position);
      Handles.DrawLine(transform.position, (lineD * _viewRadius) + transform.position);
   }
}
