using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] public FallDetection FallDetection;
    [SerializeField] public Vector3 Position;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        transform.position = Position;

    }

   /* private void CheckCondition()
    {
        if (!FallDetection.floorContact)
        {
            
            FallDetection.floorContact = false;
        }
    }*/
}
