using System;
using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    public GameObject DoorObject;

    private bool _open = false;
    public float OpenTime = 1;

    protected override void OnInteract(PlayerController player)
    {
        Vector3 a = player.transform.position - DoorObject.transform.position;
        var angle = Vector3.SignedAngle(a.normalized, -DoorObject.transform.forward, DoorObject.transform.up);
        float rot = 0;
        if (angle < 0)
        {
            rot = -90;
        }
        else
        {
            rot = 90;
        }


        StartCoroutine(OpenAnimation(rot, !_open ? 1 : -1, OpenTime));

        _open = !_open;
    }

    IEnumerator OpenAnimation(float rot, int dir, float openTime)
    {
        int steps = (int) (Mathf.Abs(rot) * 2);
        float waitTime = openTime / steps;
        float delta = rot / steps;
        while (steps > 0)
        {
            DoorObject.transform.Rotate(Vector3.up, dir * delta, Space.World);

            yield return new WaitForSeconds(waitTime);
            --steps;
        }
    }
}