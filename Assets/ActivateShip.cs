using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateShip : MonoBehaviour
{
    public InputActionReference menuButton;
    [SerializeField] private ShipController _shipController;

    private void Start()
    {
        menuButton.action.started += MenuButtonPressed;
    }

    private void MenuButtonPressed(InputAction.CallbackContext obj)
    {
        if (_shipController.enabled)
        {
            _shipController.ActivateShip();
        }
        
    }
}
