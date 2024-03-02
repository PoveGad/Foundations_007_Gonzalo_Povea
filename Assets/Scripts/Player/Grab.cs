using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Transform Holdposition;
    
    [SerializeField] private float range =2f;
    [SerializeField] private float _throwForce = 20f;
    [SerializeField] private float _snapSpeed = 40f;

    private Rigidbody _grabbedObject;
    private bool _grabPressed = false;

    private void FixedUpdate()
    {
        if (_grabbedObject)
        {
            _grabbedObject.velocity = (Holdposition.position - _grabbedObject.transform.position) * _snapSpeed;
        }
    }

    private void OnGrab()
    {
        if (_grabPressed)
        {
            _grabPressed = false;
            Debug.Log("Grab Released");
            if(!_grabbedObject) return;

            DropGrabbedObject();
        }
        else
        {
            _grabPressed = true;
            Debug.Log("Grab Pressed");
            if (Physics.Raycast(_cameraPosition.position, _cameraPosition.forward, out RaycastHit Hit, range))
            {
                if (!Hit.transform.gameObject.CompareTag("Grabbable")) return;
                
                _grabbedObject = Hit.transform.GetComponent<Rigidbody>();
                _grabbedObject.transform.parent = Holdposition;
            }
        }
    }

    private void DropGrabbedObject()
    {
        _grabbedObject.transform.parent = null;
        _grabbedObject = null;
    }

    private void OnThrow()
    {
        if(!_grabbedObject) return;
        _grabbedObject.AddForce(_cameraPosition.forward * _throwForce, ForceMode.Impulse);
        DropGrabbedObject();
    }
}
