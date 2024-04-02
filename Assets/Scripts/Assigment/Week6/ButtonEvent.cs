using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ButtonEvent : MonoBehaviour
{
    [SerializeField] private float deadTime = .5f;
    [SerializeField] private bool deadTimeActive = false;
    public UnityEvent onPressed;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && !deadTimeActive)
        {
            onPressed.Invoke();
            StartCoroutine(WaitForDeadTime());
        }
    }

    IEnumerator WaitForDeadTime()
    {
        deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        deadTimeActive = false;
    }
}
