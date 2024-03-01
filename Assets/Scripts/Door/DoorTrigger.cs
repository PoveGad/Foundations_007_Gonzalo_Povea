using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _Door;
    
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.GetComponent<DoorInteractor>())
        {
            _Door.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DoorInteractor>())
        {
            _Door.SetActive(true);
        }
    }
}
