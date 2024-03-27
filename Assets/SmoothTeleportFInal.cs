using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothTeleportFInal : BaseTeleportationInteractable
{
    [SerializeField] private GameObject Vignette;
    public float transitionDuration = 1.0f;
    [SerializeField] private GameObject XRRig;
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        SendTeleportRequest2(args.interactor);
    }
    protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
    {
        teleportRequest.destinationPosition = raycastHit.point;
        teleportRequest.destinationRotation = transform.rotation;
        return true;
    }

    void SendTeleportRequest2(XRBaseInteractor interactor)
    {
        var rayInt = interactor as XRRayInteractor;
        
        if (rayInt != null)
        {
            
            if (rayInt.TryGetCurrent3DRaycastHit(out var raycastHit))
            {
                

                var found = false;
                foreach (var interactionCollider in colliders)
                {
                    if (interactionCollider == raycastHit.collider)
                    {
                        
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    var tr = new TeleportRequest
                    {
                        requestTime = Time.time,
                    };
                    
                   
                    if (GenerateTeleportRequest(interactor, raycastHit, ref tr))
                    {
                        
                        StartCoroutine(SmoothTeleportationCouritne(tr.destinationPosition, XRRig.transform.position));
                    }
                }
            }
        }
    }

    IEnumerator SmoothTeleportationCouritne(Vector3 RayCastPosition, Vector3 interactor)
    {
       Vignette.SetActive(true);

        float timeElapsed = 0;

        while (timeElapsed < transitionDuration)
        {
            XRRig.transform.position = Vector3.Lerp(interactor, RayCastPosition, timeElapsed / transitionDuration);
            timeElapsed += Time.deltaTime;
            yield return null; 
        }

        XRRig.transform.transform.position = RayCastPosition;
        Vignette.SetActive(false);
    }
}
