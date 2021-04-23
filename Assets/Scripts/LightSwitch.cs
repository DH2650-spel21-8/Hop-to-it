using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Activateable
{
    public Light Light;

    public override void OnActivate(PlayerController player)
    {
        if (Toggle)
        {
            Light.enabled = !Light.enabled;
        }
        else
        {
            Light.enabled = true;
        }
    }
}
