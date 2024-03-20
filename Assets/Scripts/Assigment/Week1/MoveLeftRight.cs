using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float velocity = 1.0f;
    private float time;

    void Update()
    {
        
        time += Time.deltaTime * velocity;
        
        transform.position = Vector3.Lerp(pointA.position, pointB.position, Mathf.PingPong(time, 1));
    }
}
