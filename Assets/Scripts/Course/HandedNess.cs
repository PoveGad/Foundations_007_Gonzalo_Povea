using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Handed
{
    Left,
    Right
}
public class HandedNess : MonoBehaviour
{
    

    public Handed handed;
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private GameObject[] leftHandedObjects;
    [SerializeField] private GameObject[] rightHandedObjects;

    private void Start()
    {
        handed = _GameManager._handedness;
        if (handed == Handed.Left)
        {
            foreach (var obj in leftHandedObjects)
            {
                obj.SetActive(true);
            }

            foreach (var obj in rightHandedObjects)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (var obj in leftHandedObjects)
            {
                obj.SetActive(false);
            }

            foreach (var obj in rightHandedObjects)
            {
                obj.SetActive(true);
            }
        }
    }
}
