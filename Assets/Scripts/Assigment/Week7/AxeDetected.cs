using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDetected : MonoBehaviour
{
    [SerializeField] private ManagerWeek6 _week6;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _week6.EndGame();
        }
    }
}
