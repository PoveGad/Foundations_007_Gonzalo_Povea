using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
   [SerializeField] private GameObject _ship;
   [SerializeField] private float change = 1;
   private bool entered = false;
   private GameObject hand;
   private Vector3 initialHandPosition;
   private Quaternion initialHandRotation;
   private Vector3 initialPositionOffset;
   private Quaternion initialRotationOffset;
   private Rigidbody RB;

   public bool enable= false;

   private void Start()
   {
      RB= _ship.GetComponent<Rigidbody>();
   }


   private void OnTriggerEnter(Collider other)
   {
      
      if (other.CompareTag("Hand"))
      {
         enable = true;
         hand = other.gameObject;
         initialRotationOffset = Quaternion.Inverse(other.transform.rotation) * _ship.transform.rotation;
         
      }
   }

   public void ActivateShip()
   {
      RB.useGravity = false;
      _ship.transform.position += Vector3.up * 1.5f;
      entered = true;
      enable = false;
   }

   private void Update()
   {
      if (entered)
      {
         Vector3 posicionRelativaMano = hand.transform.position - transform.position;
         Vector3 direccionMovimiento = posicionRelativaMano.normalized;
         float distanciaDesdeOrigen = posicionRelativaMano.magnitude;
         Vector3 movimientoNave = direccionMovimiento * distanciaDesdeOrigen * 0.04f;

         
         _ship.transform.position += movimientoNave ;
         Quaternion newRotation = hand.transform.rotation * initialRotationOffset;
         _ship.transform.rotation = newRotation;



      }
   }


   private void OnTriggerExit(Collider other)
   {
      Desactivate();
   }

   public void Desactivate()
   {
      entered = false;
      RB.useGravity = true;
      hand = null;
      enable = false;
   }
}
