using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinCanToss : MonoBehaviour
{
    [SerializeField] private int _ballCounter;
    [SerializeField] private GameObject _canCount;
    [SerializeField] private GameObject _canva;

    private void Start()
    {
        CanCount();

        
    }

    private void CanCount()
    {
        throw new NotImplementedException();
    }

    public void CanDetected()
    {

    }
    
}
