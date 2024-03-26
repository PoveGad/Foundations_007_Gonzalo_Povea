using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFireOrStopFire : MonoBehaviour
{
    public enum FireorWater
    {
        Stopper =0,
        Starter =1,
        
    }

    [SerializeField] private FireorWater _fireorWater;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Flammable _flammable))
        {
            if (_fireorWater == FireorWater.Starter)
            {
                _flammable.StartFire();
            }
            else
            {
                _flammable.StopFire();
            }
        }
    }
}
