using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtachMine : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("GameScenary"))
        {
            if (other.contacts.Length > 0){
                Rigidbody RB = GetComponent<Rigidbody>();
                Vector3 normal = other.contacts[0].normal;
                Quaternion rotationToNormal = Quaternion.FromToRotation(Vector3.up, normal);
                transform.rotation = rotationToNormal;
                RB.constraints = RigidbodyConstraints.FreezeAll;

            }
        }
    }
}
