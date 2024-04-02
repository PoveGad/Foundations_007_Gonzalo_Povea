using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
   [SerializeField] private float RotationAmount = 90f;
   [SerializeField] private float RotationSpeed=0.3f;
   private Quaternion targetRotation;
   public bool IsOpen=false;
   private void Update()
   {
        
      if (IsOpen)
      {
         OpenDoor();
      }
      else
      {
         CloseDoor();;
      }
   }
   private void CloseDoor()
   {
      Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
      if (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
      {
         RotateDoor(targetRotation);
         Debug.Log("Se cierra");
      }
   }

   private void OpenDoor()
   {
      Quaternion targetRotation = Quaternion.Euler(0, RotationAmount, 0);
      if (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
      {
         RotateDoor(targetRotation);
         Debug.Log("Se abre");
      }

   }
   private void RotateDoor(Quaternion targetRotation)
   {
      transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed);
   }
}
