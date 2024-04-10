using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform Dial;
    [SerializeField] private int SnapAmount = 15;
    [SerializeField] private float AngleTolerance;

    private float _valuefixed;

    private XRGrabInteractable grabInteractor => GetComponent<XRGrabInteractable>();
    private XRBaseInteractor interactor;
    private bool grabbing;
    private float startangle;

    private void Start()
    {
        grabInteractor.selectEntered.AddListener(Grabbed);
        grabInteractor.selectExited.AddListener(GrabbedExited);
        
    }

    private void GrabbedExited(SelectExitEventArgs arg0)
    {
        grabbing = false;
    }

    private void Grabbed(SelectEnterEventArgs arg0)
    {
        interactor = GetComponent<XRGrabInteractable>().selectingInteractor;
        grabbing = true;
        startangle = 0f;
    }

    private void Update()
    {
        if (grabbing)
        {
            var rotationAngle = GetInteractorRoation();
            GetRotationDistance(rotationAngle);
        }
    }

    

    public float GetInteractorRoation() => interactor.GetComponent<Transform>().eulerAngles.z;

    private void GetRotationDistance(float currentAngle)
    {
        var angleDifference = Mathf.Abs(0f - currentAngle);
        _valuefixed = angleDifference;
        Debug.Log(angleDifference);
        
    }
    
    
}
