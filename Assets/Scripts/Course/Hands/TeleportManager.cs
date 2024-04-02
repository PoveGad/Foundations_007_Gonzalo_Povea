using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _teleporController;
    [SerializeField] private XRBaseInteractor _mainController;
    
    [SerializeField] private Animator _handAnimator;
    [SerializeField] private GameObject _pointer;

    private void Start()
    {
        _teleporController.selectEntered.AddListener(MoveSelectionMainController);
    }

    private void MoveSelectionMainController(SelectEnterEventArgs arg0)
    {
        var interactable = arg0.interactable;
        if(arg0.interactable is BaseTeleportationInteractable) return;
        if(arg0.interactable) _teleporController.interactionManager.ForceSelect(_mainController, interactable);
    }

    private void Update()
    {
        _pointer.SetActive(_handAnimator.GetCurrentAnimatorStateInfo(0).IsName("Point") && _handAnimator.gameObject.activeSelf);
    }
}
