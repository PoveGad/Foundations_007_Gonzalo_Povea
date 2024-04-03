using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeofGun
{
    Pull,
    Push,
}
public class GravityGun : MonoBehaviour
{
    private bool grabbed= false;
    [SerializeField] private Transform Barrel;
    [SerializeField] private float _maxDistance = 3f;
    [SerializeField] private TypeofGun _guntype;
    private bool _activated = false;
    [SerializeField] private float attractionForce = 1f;
    private List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();

    private Vector3 HitPoint;

    private void Update()
    {
        if (grabbed)
        {
            CastRayAtraction();
        }
    }

    private void CastRayAtraction()
    {
        RaycastHit hit;
        Vector3 attractPoint = Barrel.position + Barrel.forward * _maxDistance;

        if (Physics.Raycast(Barrel.position, Barrel.forward, out hit, _maxDistance))
        {
            Debug.DrawLine(Barrel.position,hit.point,Color.blue);
            attractPoint = hit.point;
            
        }
        Debug.DrawLine(Barrel.position, Barrel.forward * _maxDistance, Color.blue); 
        HitPoint = attractPoint;
        Collider[] objectsInRange = Physics.OverlapSphere(attractPoint, 0.5f);
        Debug.Log(objectsInRange.Length);
        if (objectsInRange.Length > 0)
        {
            if (_activated)
            {
                AttractObjects(attractPoint, objectsInRange);
            }
            if(!_activated)
            {
                ResetAffectedObjectsGravity();
            }
        }
    }



    void AttractObjects(Vector3 center, Collider[] objectsInRange)
    {
        foreach (var obj in objectsInRange)
        {
            Debug.Log(obj.gameObject);
            Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (!affectedRigidbodies.Contains(rb))
                {
                    rb.useGravity = false; 
                    affectedRigidbodies.Add(rb); 
                }

                Vector3 direction = Barrel.position - rb.position;
                Vector3 direction2 = center - rb.position;

                rb.AddForce(direction.normalized * attractionForce * Time.deltaTime, ForceMode.VelocityChange);
                rb.AddForce(direction2.normalized * attractionForce * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }

    void ResetAffectedObjectsGravity()
    {

        List<Rigidbody> stillAffected = new List<Rigidbody>();
        foreach (var rb in affectedRigidbodies)
        {
            if (rb != null)
            {
                Collider[] objectsInRange = Physics.OverlapSphere(rb.position, 2f);
                if (objectsInRange.Length == 0)
                {
                    rb.useGravity = true;
                }
                else
                {
                    stillAffected.Add(rb);
                }
            }
        }
        affectedRigidbodies = stillAffected;
    }

    public void Selected()
    {
        grabbed = true;
    }

    public void Unselected()
    {
        grabbed = false;
    }

    public void _Activated()
    {
        _activated = true;
    }

    public void _Deactivated()
    {
        _activated = false;
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
       
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(HitPoint, 0.5f);

    }
#endif
}
