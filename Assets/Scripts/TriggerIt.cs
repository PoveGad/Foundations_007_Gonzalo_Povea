using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIt : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private ParticleSystem _particleSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.SetActive(true);
            _particleSystem.Play();
        }
    }
}
