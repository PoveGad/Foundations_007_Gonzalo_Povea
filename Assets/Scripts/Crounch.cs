using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crounch : MonoBehaviour
{
    [SerializeField] private CharacterController _charControoler;
    [SerializeField] private float _crouchHeight = 1;
    private float _originalHeight;
    private bool _crouched = false;
    
    void Start()
    {
        _originalHeight = _charControoler.height;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCrouch()
    {
        if (_crouched)
        {
            _crouched = false;
            _charControoler.height = _originalHeight;
            Debug.Log("Played get up");
        }
        else
        {
            _crouched = true;
            _charControoler.height = _crouchHeight;
            Debug.Log("Player crouched");
        }
       
    }
}
