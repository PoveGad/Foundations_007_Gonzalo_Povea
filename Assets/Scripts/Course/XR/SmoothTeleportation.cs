using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothTeleportation : BaseTeleportationInteractable
{
    [SerializeField] private float teleportTime = 1.5f;
    [SerializeField] private float _stoppingDistance = 0.1f;
    
    private Vector3 TeleportFinished;
    private bool _isTeleporting;
    private XRRig _rig;
    private XRCustomTeleportationProvider _teleportationProvider;
 
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        BeginTelerpot(args.interactor);
    }

    private void BeginTelerpot(XRBaseInteractor argsInteractor)
    {
       _rig = argsInteractor.GetComponentInParent<XRRig>();
       _teleportationProvider = _rig.GetComponent<XRCustomTeleportationProvider>();
       if(_teleportationProvider.isTeleporing) return;
       _teleportationProvider.TeleportBegin();
       var interactorPos = argsInteractor.transform.localPosition;
       interactorPos.y = 0;
       TeleportFinished = transform.position - interactorPos;
       _isTeleporting = true;
    }

   

    // Update is called once per frame
    void Update()
    {
        if (_isTeleporting)
        {

            _rig.transform.position = Vector3.MoveTowards(_rig.transform.position, TeleportFinished, teleportTime);
            if (Vector3.Distance(_rig.transform.position, TeleportFinished)< _stoppingDistance)
            {
                _isTeleporting = false;
                _teleportationProvider.TeleportEnd();

            }
        }
    }
}
