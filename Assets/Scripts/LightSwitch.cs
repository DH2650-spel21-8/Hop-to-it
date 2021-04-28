using UnityEngine;

public class LightSwitch : Activateable
{
    public Light Light;

    protected override void OnActivate(PlayerController player)
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
