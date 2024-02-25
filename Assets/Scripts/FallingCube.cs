using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class FallingCube : MonoBehaviour
{
    private bool counter=false;
    private float time = 0f;
    private int seconds=0;
    [SerializeField] private TMP_Text timeUI;
    [SerializeField] private int Trigger=10;
    [SerializeField] private GameObject cube;
    [SerializeField] private Transform cubeposition;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            counter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            counter = false;
            time = 0f;
            seconds = 0;
            timeUI.text = "0";
        }
    }

    private void Update()
    {
        if (counter)
        {
            time += Time.deltaTime;
            if (time >= 1f)
            {
                seconds++;
                time -= 1f;
                timeUI.text = seconds.ToString();
                if (seconds >= Trigger)
                {
                    SpawnObject();
                    time = 0;
                    seconds = 0;
                }
            }


        }
    }

    private void SpawnObject()
    {
        Instantiate(cube,cubeposition.position, cubeposition.rotation);
    }
}
