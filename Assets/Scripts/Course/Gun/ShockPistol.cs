using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class ShockPistol : Gun
{
    [SerializeField] private Renderer[] _gunRenderers;
    [SerializeField] private Material[] _ammoScreenMaterials;
    [SerializeField] private Projection _projection;
    
    private bool startProyection = false;

    private void Update()
    {
        if (startProyection)
        {
            if(!CanFire()) return;
            _projection.SimulateTrajectory(_ammoClip.bulletObject, _gunBarrel, _gunBarrel.forward*_ammoClip.bulletSpeed);
        }
    }

    protected override void Start()
    {
        var activeAmmoSocket = GetComponentInChildren<XRTagLimitedSocketInteractor>();
        _ammoSocket = activeAmmoSocket;
        base.Start();
        Assert.IsNotNull(_gunRenderers, "You have not assigned a renderer to gun" + name);
        Assert.IsNotNull(_ammoScreenMaterials, "You have not assigned materials"+ name);
    }

    protected override void AmmoDetached(SelectExitEventArgs arg0)
    {
        base.AmmoDetached(arg0);
        UpdateShockPistolScreen();
    }

    protected override void AmmoAttached(SelectEnterEventArgs arg0)
    {
        base.AmmoAttached(arg0);
        UpdateShockPistolScreen();
    }

    protected override void Simulate(ActivateEventArgs arg0)
    {
        startProyection = true;
    }

    protected override void Fire(DeactivateEventArgs arg0)
    {
        if(!CanFire()) return;
        base.Fire(arg0);
        UpdateShockPistolScreen();
        var bullet = Instantiate(_ammoClip.bulletObject, _gunBarrel.position, Quaternion.identity)
            .GetComponent<Rigidbody>();
        bullet.AddForce(_gunBarrel.forward*_ammoClip.bulletSpeed, ForceMode.Impulse);
        Destroy(bullet, 4f);
        startProyection = false;
    }


    private void UpdateShockPistolScreen()
    {
        if (!_ammoClip)
        {
            AssignScreenMaterial(_ammoScreenMaterials[0]);
            return;
        }
        AssignScreenMaterial(_ammoScreenMaterials[_ammoClip.amount]);
    }

    private void AssignScreenMaterial(Material newMaterial)
    {
        foreach (var rend in _gunRenderers)
        {
            if(!rend.gameObject.activeSelf) continue;
            var mats = rend.materials;
            mats[1] = newMaterial;
            rend.materials = mats;
            
        }
        
    }

    
}
