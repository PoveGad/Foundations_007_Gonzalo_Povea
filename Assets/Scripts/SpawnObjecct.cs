using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnObjecct : XRBaseInteractable
{

    // Start is called before the first frame update
    [SerializeField] private GameObject prefabGenerico;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        CreateAndSelectObject(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {

    }

    private void CreateAndSelectObject(SelectEnterEventArgs args)
    {
        GameObject instantiatedObject = CreateObject(args.interactor.transform);
        interactionManager.SelectEnter(args.interactor, instantiatedObject.GetComponent<XRGrabInteractable>());

    }


    private GameObject CreateObject(Transform orientation)
    {
        GameObject instantiatedObject = Instantiate(prefabGenerico, orientation.position, orientation.rotation);

        return instantiatedObject;
    }
}
