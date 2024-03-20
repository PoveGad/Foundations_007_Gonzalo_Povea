using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    [SerializeField] private GameObject BallProyectile;
    [SerializeField] private Transform StartPointProyectile;
    [SerializeField] private float _Initialspeed = 1000f;

    public void Shoot()
    {
        GameObject _BallProyectile = Instantiate(BallProyectile, StartPointProyectile.position,
            StartPointProyectile.rotation);
        
        if (_BallProyectile.TryGetComponent(out Rigidbody rigidbody))
        {
            InitialForce(rigidbody);
        }
    }

    private void InitialForce(Rigidbody rigidbody)
    {
        
        Vector3 force = StartPointProyectile.forward * _Initialspeed;
       
        rigidbody.AddForce(StartPointProyectile.forward * 500);
    }
}
