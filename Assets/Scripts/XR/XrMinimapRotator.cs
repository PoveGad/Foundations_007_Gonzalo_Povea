using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XR
{


    public class XrMinimapRotator : MonoBehaviour
    {
        [SerializeField] private Transform _leftHandTransform;
        [SerializeField] private Transform _rightHandTransform;
        [SerializeField] private Transform _rotationReference;

        private Vector3 _initialRotation;
        private HandedNess _handedNess;

        private void Start()
        {
            _initialRotation = transform.eulerAngles;
            _handedNess = GetComponent<HandedNess>();
            if (_handedNess.handed == Handed.Left)
            {
                _rotationReference = _rightHandTransform;
            }
            else
            {
                _rotationReference = _leftHandTransform;
            }
        }

        private void Update()
        {
            Vector3 newrot = new Vector3(0, 0, -_rotationReference.eulerAngles.y) + _initialRotation;
            transform.rotation = Quaternion.Euler(newrot);

        }
    }
}
