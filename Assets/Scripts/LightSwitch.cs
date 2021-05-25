using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable
{
    public List<Light> Lights;

    protected override void OnInteract(PlayerController player)
    {
        if (Toggle)
        {
            foreach (Light light in Lights)
            {
                light.enabled = !light.enabled;
            }
        }
        else
        {
            foreach (Light light in Lights)
            {
                light.enabled = true;
            }
        }
    }
}
