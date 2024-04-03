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
    
    [SerializeField] private Transform Barrel;
    [SerializeField] private float _maxDistance = 3f;
    [SerializeField] private TypeofGun _guntype;
    [SerializeField] private float attractionForce = 1f;
    [SerializeField] private float radius = 0.5f;
    [SerializeField] private GameObject _Light;
    
    public LayerMask attractionLayers;
    private bool grabbed= false;
    private Vector3 HitPoint;
    private float currentdistance;
    private bool _activated = false;
    private List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();

    private void Start()
    {
        currentdistance = _maxDistance;
        _Light.SetActive(false);
    }

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
        Vector3 attractPoint = Barrel.position + Barrel.forward * currentdistance;
        Debug.DrawLine(Barrel.position, Barrel.forward * currentdistance, Color.blue); 

        if (Physics.Raycast(Barrel.position, Barrel.forward, out hit, currentdistance))
        {
            Debug.DrawLine(Barrel.position,hit.point,Color.blue);
            attractPoint = hit.point;
            
        }
        
        
        HitPoint = attractPoint;
        
        Collider[] objectsInRange = Physics.OverlapSphere(attractPoint, radius);
        
        if (objectsInRange.Length > 0)
        {
            if (_activated)
            {
                AttractObjects(attractPoint, objectsInRange);
                if (_guntype == TypeofGun.Pull)
                {

                    currentdistance = Mathf.Max(1f, currentdistance - 0.008f);
                }

                if (_guntype == TypeofGun.Push)
                {

                    currentdistance += 0.008f;
                }
            }
            else
            {
                ResetAffectedObjectsGravity();
            }
            
        }
    }



    void AttractObjects(Vector3 center, Collider[] objectsInRange)
    {
        
        foreach (var obj in objectsInRange)
        {
            
            Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (!affectedRigidbodies.Contains(rb))
                {
                    rb.useGravity = false; 
                    affectedRigidbodies.Add(rb);
                    
                }
            }
        }
        foreach (var RB in affectedRigidbodies)
        {
            Vector3 direction = (Barrel.position - RB.position).normalized;
            Vector3 direction3 = (RB.position - Barrel.position).normalized;
            
            Vector3 direction2 = (center - RB.position).normalized;
            

            //RB.AddForce(direction.normalized * attractionForce/5 /* Time.deltaTime*/, ForceMode.VelocityChange);
            //RB.AddForce(direction2.normalized * (15*attractionForce)  /* Time.deltaTime*/, ForceMode.VelocityChange);
            RB.MovePosition(RB.position + direction2 * attractionForce);

            if (_guntype == TypeofGun.Pull)
            {
                RB.MovePosition(RB.position + direction * attractionForce/5);
            }

            if (_guntype == TypeofGun.Push)
            {

                RB.MovePosition(RB.position + direction3 * attractionForce/5);
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
              rb.useGravity = true;
              affectedRigidbodies.Remove(rb);
               
            }
        }

        currentdistance = _maxDistance;

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
        _Light.SetActive(true);
    }

    public void _Deactivated()
    {
        _activated = false;
        _Light.SetActive(false);
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
       
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(HitPoint, radius);

    }
#endif
}
