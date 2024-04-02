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
        if (Physics.Raycast(Barrel.position, Barrel.forward, out hit, _maxDistance))
        {
            Debug.DrawLine(Barrel.position,hit.point,Color.blue);
            
            AttrationForce(hit.point);
        }
        else
        {
            Debug.DrawLine(Barrel.position, Barrel.position + Barrel.forward*_maxDistance);
            AttrationForce(Barrel.position + Barrel.forward*_maxDistance);
        }
    }

    private void AttrationForce(Vector3 hitPoint)
    {
        HitPoint = hitPoint;
        Collider[] Objects = Physics.OverlapSphere(hitPoint, 0.5f);
        if (_activated)
        {
            foreach (var obj in Objects)
            {
                
                if (obj.gameObject.TryGetComponent(out Rigidbody _rigidbody))
                {
                    _rigidbody.useGravity = false;
                }
                
            }
        }
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
