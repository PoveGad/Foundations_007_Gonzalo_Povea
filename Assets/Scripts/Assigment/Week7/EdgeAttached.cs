using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeAttached : MonoBehaviour
{
    public Rigidbody axeRigidbody; 
    private bool isStuck = false;
    [SerializeField] private float trigger = 1f;
    [SerializeField] private AudioSource _SFX_Effect;

    private void OnTriggerEnter(Collider other)
    {
        
        if (!isStuck && other.CompareTag("Box"))
        {
            float speed = axeRigidbody.velocity.sqrMagnitude; 
           Debug.Log(speed);
            if (speed > trigger)
            {
                axeRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                _SFX_Effect.Play();
                isStuck = true; 
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isStuck && other.CompareTag("Box"))
        {

            axeRigidbody.constraints = RigidbodyConstraints.None;
            
            isStuck = false;
        }
    }

    public void _Select()
    {
        axeRigidbody.constraints = RigidbodyConstraints.None;
        
        isStuck = false;
    
    }
}
