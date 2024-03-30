using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRHandAnimator : MonoBehaviour
{
    [SerializeField] private ActionBasedController _controller;
    [SerializeField] private Animator _animator;

    private void Start()
    {

        _controller.selectAction.action.started += Point;
        _controller.selectAction.action.canceled += PointReleased;

        _controller.activateAction.action.started += Fist;
        _controller.activateAction.action.canceled += FistReleased;
    }

    private void OnDestroy()
    {
        _controller.selectAction.action.started -= Point;
        _controller.selectAction.action.canceled -= PointReleased;

        _controller.activateAction.action.started -= Fist;
        _controller.activateAction.action.canceled -= FistReleased;
    }

    private void FistReleased(InputAction.CallbackContext obj)
    {
        
        _animator.SetBool("Fist", false);
    }

    private void Fist(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Fist", true);
    }

    private void PointReleased(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Point", false);
    }

    private void Point(InputAction.CallbackContext obj)
    {
        _animator.SetBool("Point", true);
    }
}
