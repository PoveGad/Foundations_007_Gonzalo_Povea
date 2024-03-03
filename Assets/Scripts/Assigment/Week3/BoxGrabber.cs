using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



public class BoxGrabber : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Transform Holdposition;
    [SerializeField] private GameObject Capsule;
    
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
                if (!Hit.transform.gameObject.CompareTag("Box")) return;
               
                _grabbedObject = Hit.transform.GetComponent<Rigidbody>();
                gameObject.layer = LayerMask.NameToLayer("Box");
                Capsule.layer = LayerMask.NameToLayer("Box");
                _grabbedObject.gameObject.transform.forward = transform.forward;
                _grabbedObject.gameObject.transform.parent = Holdposition;
                _grabbedObject.constraints = RigidbodyConstraints.FreezeRotation;
                
            }
        }
    }

    private void DropGrabbedObject()
    {
        _grabbedObject.gameObject.transform.position += transform.forward * 1.4f;
        _grabbedObject.constraints = RigidbodyConstraints.None;
        gameObject.layer = LayerMask.NameToLayer("Default");
        Capsule.layer = LayerMask.NameToLayer("Default");
        
        
        _grabbedObject.gameObject.transform.parent = null;
        _grabbedObject = null;

            
        
    }

    private void OnThrow()
    {
        if(!_grabbedObject) return;
        _grabbedObject.AddForce(_cameraPosition.forward * _throwForce, ForceMode.Impulse);
        DropGrabbedObject();
    }
}
