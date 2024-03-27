using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public Camera TargetCamera;
    public string Layer1 = "Minimap";
    public string Layer2 = "Water";
    private bool changed = false;

    public void ChangeScreen()
    {
        if (!changed)
        {
            
            TargetCamera.cullingMask |= 1 << LayerMask.NameToLayer(Layer2);
            TargetCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(Layer1));
            changed = true;
            return;
        }

        if (changed)
        {
            TargetCamera.cullingMask |= 1 << LayerMask.NameToLayer(Layer1);
            TargetCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(Layer2));
            changed = false;
        }
    }
}
