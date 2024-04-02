using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Identification : MonoBehaviour
{
    [SerializeField] private ColorKey _ColorKeyDoor;
    [SerializeField] private DoorManager Door;
    
    private Rigidbody Key;
    private float time = 0f;
    private bool _IsOpen;
    private void Update()
    {
        
        if (_IsOpen)
        {
            time += Time.deltaTime;
            if (time <= 1f)
            {
                SetKeyPosition();
            }
        }
        
    }

    private void SetKeyPosition()
    {
        Key.transform.position = transform.position;
        Key.transform.rotation = transform.rotation;
        Key.constraints = RigidbodyConstraints.FreezeAll;
        
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out KeyIdentificator keyIdentificator))
        {
            if (keyIdentificator.colorKey == _ColorKeyDoor)
            {
                Key = keyIdentificator.gameObject.GetComponent<Rigidbody>();
                Key.constraints = RigidbodyConstraints.FreezeAll;
                Door.IsOpen = true;
                _IsOpen = true;


            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out KeyIdentificator keyIdentificator))
        {
            if (keyIdentificator.colorKey == _ColorKeyDoor)
            {
                Door.IsOpen = false;
                _IsOpen = false;
                time = 0f;
            }
        }
    }
}
