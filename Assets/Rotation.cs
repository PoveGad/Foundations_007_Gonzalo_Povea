using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform Dial;
    [SerializeField] private int SnapAmount = 15;
    [SerializeField] private float AngleTolerance;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _text2;
    private int number;
    [SerializeField] private GameObject _dial;
    
    

    private float _valuefixed;
    private float startAngleInteractor;
    private int i =0;

    private XRGrabInteractable grabInteractor => GetComponent<XRGrabInteractable>();
    private XRBaseInteractor interactor;
    private bool grabbing;
    private float startangle;
    

    private void Start()
    {
        GenerateNumber();
        grabInteractor.selectEntered.AddListener(Grabbed);
        grabInteractor.selectExited.AddListener(GrabbedExited);
        _text.text = "0";
        

    }

    private void GenerateNumber()
    {
        number = Random.Range(1, 360);
        _text2.text = "Password: " + number.ToString();
    }

    private void GrabbedExited(SelectExitEventArgs arg0)
    {
        grabbing = false;
    }

    private void Grabbed(SelectEnterEventArgs arg0)
    {
        interactor = GetComponent<XRGrabInteractable>().selectingInteractor;
        grabbing = true;
        startangle = Dial.rotation.eulerAngles.z;
        startAngleInteractor = GetInteractorRoation();
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
        var angleDifference = Mathf.Abs(startAngleInteractor - currentAngle);
        var newDialRotation = startangle + angleDifference;
        Dial.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,
            newDialRotation);
        
        _text.text = ((int)Dial.rotation.eulerAngles.z).ToString();
       
        if ((int)Dial.rotation.eulerAngles.z == number)
        {
            i++;
            if (i == 3)
            {
                UnlockDoor();
            }
            else
            {
                GenerateNumber();
            }
        }
    }

    private void UnlockDoor()
    {
        _dial.SetActive(false);
        _text2.text = "Unlocked";
    }
}
