using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetectionAssigment : MonoBehaviour
{
    [SerializeField] private TinCanToss manager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cylinder"))
        {


        }
    }
}
