using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTarget : MonoBehaviour
{
    [SerializeField] private GameTwoManager _manager;
    [SerializeField] private GameObject DestructiveEffect;
    [SerializeField] private GameObject BallEffect;
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Transform Model;
    public bool activated = false;

    private void OnTriggerEnter(Collider other){
        if(activated) return;
        if (other.gameObject.CompareTag("Grabbable"))
        {
            CheckBaseTarget();
            DeleteObject(other);
        }
    }

    public void DeleteObject(Collider other)
    {
        Instantiate(BallEffect, other.gameObject.transform.position, other.gameObject.transform.rotation);
        Destroy(other.gameObject);
    }

    private void CheckBaseTarget()
    {
        _manager.BaseTargetHit();
        DeletePrefab();
    }

    private void DeletePrefab()
    {
        Instantiate(DestructiveEffect, Model.position, Model.rotation);
        Prefab.SetActive(false);
    }

    public void CheckCenterTarget()
    {
        _manager.CenterTargetHit();
        DeletePrefab();
    }
}
