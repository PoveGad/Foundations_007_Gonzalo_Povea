using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTarget : MonoBehaviour
{
    [SerializeField] private BaseTarget _baseTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            _baseTarget.activated = true;
            _baseTarget.DeleteObject(other);
            _baseTarget.CheckCenterTarget();

        }
    }
}
