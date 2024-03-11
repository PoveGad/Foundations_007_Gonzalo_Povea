using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotExplotionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] Barrels;
    [SerializeField] private GameObject _ExplotionParticle;
    [SerializeField] private AudioSource explotion;
    
    private void Start()
    {
        explotion = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RobotExplotion robotExplotion))
        {
            explotion.Play();
            robotExplotion.ExploteRobot();
            foreach (GameObject Barrel in Barrels)
            {
                Instantiate(_ExplotionParticle, Barrel.transform.position, Quaternion.identity);
                Destroy(Barrel);
                
            }
            Destroy(gameObject);
        }
        
    }
}
