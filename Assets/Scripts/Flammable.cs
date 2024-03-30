using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    [SerializeField] private GameObject Fire;

    private void Start()
    {
        Fire.SetActive(false);
    }

    public void StartFire()
    {
        Fire.SetActive(true);
    }

    public void StopFire()
    {
        Fire.SetActive(false);
    }
}
