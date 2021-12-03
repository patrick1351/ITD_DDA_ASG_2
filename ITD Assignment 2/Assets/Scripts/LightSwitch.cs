using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    // THe light that will be turned on/off
    public Light lightToggle;

    // This will turn on/off ligt
    public void ToggleLight()
    {
        lightToggle.enabled = !lightToggle.enabled;
    }
}
