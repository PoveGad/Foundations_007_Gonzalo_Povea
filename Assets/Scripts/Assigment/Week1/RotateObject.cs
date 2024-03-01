using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 100.0f;

    void Update()
    {
        // Rota el objeto alrededor de su eje Y a la velocidad especificada
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
