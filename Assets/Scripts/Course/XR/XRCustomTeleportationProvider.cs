using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRCustomTeleportationProvider : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _leftHandInteractor;
    [SerializeField] private XRBaseInteractor _rightHandInteractor;
    [SerializeField] private Animator _vignetteAnimator;

    private Transform _leftHandCurrentSelection;
    private Transform _rightHandCurrentSelection;
    public bool isTeleporing;


    private void Start()
    {
        _leftHandInteractor.selectEntered.AddListener(LeftHandEnter);
        _leftHandInteractor.selectExited.AddListener(LeftHandExit);
        _rightHandInteractor.selectEntered.AddListener(RightHandEnter);
        _rightHandInteractor.selectExited.AddListener(RightHandExit);
    }

    private void RightHandExit(SelectExitEventArgs arg0)
    {
        _rightHandCurrentSelection = UnbidGrab(_rightHandCurrentSelection);
    }

    private void RightHandEnter(SelectEnterEventArgs arg0)
    {
        _rightHandCurrentSelection = BindGrab(arg0.interactable);
    }

    private void LeftHandExit(SelectExitEventArgs arg0)
    {
        _leftHandCurrentSelection = UnbidGrab(_leftHandCurrentSelection);
    }

    private void LeftHandEnter(SelectEnterEventArgs arg0)
    {
        _leftHandCurrentSelection = BindGrab(arg0.interactable);
    }

    private Transform BindGrab(XRBaseInteractable interactable)
    {
        if (interactable is XRGrabInteractable)
        {
            return interactable.transform;
        }

        return null;
    }

    private Transform UnbidGrab(Transform currentSelection)
    {
        currentSelection.parent = null;
        return null;
    }

    private void ParentInteractable(XRBaseInteractor interactor, Transform currentSelection)
    {
        if (currentSelection) currentSelection.parent = interactor.transform;
    }

    private void UnparentInteractable(XRBaseInteractor interactor, Transform currentSelection)
    {
        if (currentSelection) currentSelection.parent = null;
    }

    public void TeleportBegin()
    {
        isTeleporing = true;
        _vignetteAnimator.SetBool("isTeleporting", isTeleporing);
        ParentInteractable(_rightHandInteractor, _rightHandCurrentSelection);
        ParentInteractable(_leftHandInteractor, _rightHandCurrentSelection);
        
    }

    public void TeleportEnd()
    {
        isTeleporing = false;
        _vignetteAnimator.SetBool("isTeleporting", isTeleporing);
        UnparentInteractable(_rightHandInteractor, _rightHandCurrentSelection);
        UnparentInteractable(_leftHandInteractor, _rightHandCurrentSelection);
    }
}
