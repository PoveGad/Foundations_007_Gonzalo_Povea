using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRiggerit_OnlyLog : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
     if(other.CompareTag("Player"))
     {
         Debug.Log("YOU WIN");
     }
   }
}
