using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Hands
{
    public class XrHandHider : MonoBehaviour
    {
        [SerializeField] private XRBaseControllerInteractor _controller;
        [SerializeField] private Rigidbody _handRigidBody;
        [SerializeField] private ConfigurableJoint _configurable;
        [SerializeField] private float _delay = 0.15f;

        private Quaternion _orignalHandRot;

        private void Start()
        {
            _controller.selectEntered.AddListener(SelectEntered);
            _controller.selectExited.AddListener(SelectExited);
            _orignalHandRot = _handRigidBody.transform.localRotation;

        }

        private void SelectExited(SelectExitEventArgs arg0)
        {
            if(arg0.interactable is BaseTeleportationInteractable) return;
            Invoke(nameof(ShowHand), _delay);
        }

        private void SelectEntered(SelectEnterEventArgs arg0)
        {
            if(arg0.interactable is BaseTeleportationInteractable) return;
            _handRigidBody.gameObject.SetActive(false);
            _configurable.connectedBody = null;
            CancelInvoke(nameof(ShowHand));
        }

        private void ShowHand()
        {
            _handRigidBody.gameObject.SetActive(true);
            _handRigidBody.transform.position = _controller.transform.position;
            _handRigidBody.transform.rotation =
                Quaternion.Euler(_controller.transform.eulerAngles + _orignalHandRot.eulerAngles);
            _configurable.connectedBody = _handRigidBody;
        }
    }
}

