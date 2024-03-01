using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float velocity = 1.0f;
    private float time;

    void Update()
    {
        
        time += Time.deltaTime * velocity;
        
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(time, 1));
    }
}
