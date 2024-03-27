using System;
using System.Collections;

namespace UnityEngine.XR.Interaction.Toolkit
{
    using UnityEngine;

    [HelpURL(XRHelpURLConstants.k_TeleportationArea)]
    public class SmoothTeleport : BaseTeleportationInteractable
    {
        [SerializeField] private GameObject XRRig;
        private Transform InitialPosition;
        private Vector3 FinalPosition;
        protected override bool GenerateTeleportRequest(XRBaseInteractor interactor, RaycastHit raycastHit, ref TeleportRequest teleportRequest)
        {
            InitialPosition = XRRig.transform;
            FinalPosition = raycastHit.point;
            StartCoroutine(_SmoothTeleport(InitialPosition, FinalPosition));
            teleportRequest.destinationPosition = raycastHit.point;
            teleportRequest.destinationRotation = transform.rotation;
            return true;
        }

        IEnumerator _SmoothTeleport(Transform interactor, Vector3 RayCastHit)
        {
            Vector3 startPosition = interactor.position; 
            Vector3 endPosition = RayCastHit; 
            float threshold = 0.01f; 
            float timeElapsed = 0;

            while (Vector3.Distance(interactor.position, endPosition) < threshold)
            {
                interactor.position = Vector3.Lerp(startPosition, endPosition, timeElapsed);
                timeElapsed += Time.deltaTime;
                yield return null; 
            }
            interactor.position = endPosition; 
        }
    }
}
