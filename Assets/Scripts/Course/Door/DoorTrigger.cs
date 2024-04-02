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
           OpenDoor();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DoorInteractor>())
        {
            CloseDoor();
        }
    }

    protected void OpenDoor()
    {
        _Door.SetActive(false);
        
    }

    protected void CloseDoor()
    {
        _Door.SetActive(true);
    }
}
