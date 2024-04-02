using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimation : MonoBehaviour
{
    [SerializeField] private float angle = 60f;
    public float speed = 2.0f;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private Quaternion endRotation;

    private void Start()
    {
        startRotation = transform.rotation;
        endRotation = startRotation * Quaternion.Euler(0, angle, 0);
    }

    private void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
    }
}
