using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIt : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has achieve goal");
        }
    }
}
