using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityEngine.XR.Interaction.Toolkit
{[HelpURL(XRHelpURLConstants.k_TeleportationArea)]
public class SmoothTeleport2 : TeleportationArea
{
    public float transitionDuration = 4.0f; 
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private Vector3 IDK;

    protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
    {
        IDK = raycastHit.point;
        return true;
    }
    
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        
        StartCoroutine(SmoothTeleport(args.interactor.transform)); 
    }

    private IEnumerator SmoothTeleport(Transform interactor)
    {
        Vector3 startPosition = interactor.position;
        Vector3 endPosition = IDK; 
        Debug.Log("ola");

        float timeElapsed = 0;

        while (timeElapsed < transitionDuration)
        {
            interactor.position = Vector3.Lerp(startPosition, endPosition, transitionCurve.Evaluate(timeElapsed / transitionDuration));
            timeElapsed += Time.deltaTime;
            yield return null; 
        }

        interactor.position = endPosition; 
    }
}}
