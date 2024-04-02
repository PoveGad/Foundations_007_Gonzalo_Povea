using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GameTwoManager Manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            Manager.StartGame();
            Destroy(other.gameObject);
        }
    }
}
