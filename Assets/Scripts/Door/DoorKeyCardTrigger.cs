using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorKeyCardTrigger : DoorTrigger
{
    [SerializeField] private int _keyCardLevel = 1;
    [SerializeField] private XRSocketInteractor _Socket;
    [SerializeField] private Renderer _lightOBject;
    [SerializeField] private Light _light;
    [SerializeField] private Color _defaultColor = Color.yellow;
    [SerializeField] private Color _errorColor = Color.red;
    [SerializeField] private Color _successColor = Color.green;
    private bool _isOpen = false;

    private void Start()
    {
        SetLightColor(_defaultColor);
        _Socket.selectEntered.AddListener(KeyCardInserted);
        _Socket.selectExited.AddListener(KeyCardRemoved);
    }

    private void KeyCardRemoved(SelectExitEventArgs arg0)
    {
        SetLightColor(_defaultColor);
        _isOpen = false;
        CloseDoor();
        
    }

    private void KeyCardInserted(SelectEnterEventArgs arg0)
    {
        if (!arg0.interactable.TryGetComponent(out KeyCard keyCard))
        {
            Debug.LogWarning("No Keycard Component attached to inserted object");
            SetLightColor(_errorColor);
            return;
        }

        if (keyCard.KeycardLevel >= _keyCardLevel)
        {
            SetLightColor(_successColor);
            _isOpen = true;
            OpenDoor();
        }
        else
        {
            SetLightColor(_errorColor);
            
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if(_isOpen) return;
        base.OnTriggerExit(other);
    }

    private void SetLightColor(Color color)
    {
        _lightOBject.material.color = color;
        _light.color = color;
    }
}
