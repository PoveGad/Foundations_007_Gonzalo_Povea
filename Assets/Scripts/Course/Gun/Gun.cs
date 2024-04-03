using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable _grabInteractable;
    [SerializeField] protected Transform _gunBarrel;
    [SerializeField] protected XRSocketInteractor _ammoSocket;
    
    protected AmmoClip _ammoClip;
    
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Assert.IsNotNull(_grabInteractable, "You have not aasigned a grab itneractable to gun" + name);
        Assert.IsNotNull(_gunBarrel, "You ave not assigned a gun barrel to gun"+ name );
        Assert.IsNotNull(_ammoSocket, "You have not assigned an ammo socket to gun" + name);
        
        _ammoSocket.selectEntered.AddListener(AmmoAttached);
        _ammoSocket.selectExited.AddListener(AmmoDetached);
        _grabInteractable.deactivated.AddListener(Fire);
        _grabInteractable.activated.AddListener(Simulate);
    }

   

    protected virtual void Simulate(ActivateEventArgs arg0)
    {
        
    }

    protected virtual bool CanFire()
    {
        if (!_ammoClip) return false;
        if (_ammoClip.amount <= 0)
        {
            _ammoClip.amount = 0;
            return false;
        }
        return true;
    }

    protected virtual void Fire(DeactivateEventArgs arg0)
    {
        if(!CanFire()) return;
        _ammoClip.amount -= 1;
    }
    

    protected virtual void AmmoDetached(SelectExitEventArgs arg0)
    {
        IgnoreCollision(arg0.interactable, false);
        _ammoClip = null;
        
    }

    protected virtual void AmmoAttached(SelectEnterEventArgs arg0)
    {
        IgnoreCollision(arg0.interactable, true);
        _ammoClip = arg0.interactable.GetComponent<AmmoClip>();
        
    }

    protected virtual void IgnoreCollision(XRBaseInteractable interactable, bool ignore)
    {
        var myColliders = GetComponentsInChildren<Collider>();
        foreach (var mycollider in myColliders)
        {
            foreach (var interactableCollider in interactable.colliders)
            {
                Physics.IgnoreCollision(mycollider,interactableCollider, ignore);
            }
        }
    }

    // Update is called once per frame
    
}
