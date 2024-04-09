using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSandboxManager : MonoBehaviour
{
    [SerializeField] private Color[] _lightColors;
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Material[] _materials;

    [SerializeField] private Light _light;
    private bool gravitypressed;
    private int i=0;

    private void Start()
    {
        i = 0;
    }


    public void SwitchLight()
    {
        if (_light.enabled)
        {
            _light.enabled = false;
        }
        else
        {
            _light.enabled = true;
        }
    }

    public void ChangeMaterial()
    {
        foreach (var body in _rigidbodies)
        {
            body.gameObject.GetComponent<MeshRenderer>().material = _materials[i];
            Debug.Log("Hola");
        }
        
        i++;
        if (i == _materials.Length)
        {
            i = 0;
        }
    }

    public void ChangeGravity()
    {
        if (gravitypressed)
        {
            foreach (var body in _rigidbodies)
            {
                body.useGravity = false;
                body.transform.position += Vector3.up * 0.3f;
                body.AddTorque(Vector3.up*0.5f, ForceMode.Impulse);
            }

            gravitypressed = false;
            
        }
        else
        {

            foreach (var body in _rigidbodies)
            {
                body.useGravity = true;
                
            }

            gravitypressed = true; 
        }
    }

    public void ChangeColor(int i)
    {
        _light.color = _lightColors[i];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
