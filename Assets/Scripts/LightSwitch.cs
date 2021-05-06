using UnityEngine;

public class LightSwitch : Interactable
{
    public Light Light;

    protected override void OnInteract(PlayerController player)
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
