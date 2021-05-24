using System;
using System.Collections;
using System.Linq;
using UnityEngine;

/**
 * An interactable door in the game.
 * The player can open and close the door based on where they stand in relation to the hinge.
 * Requires that the door object pivot around its hinge for the desired effect.
 *
 * Place the Interactable's trigger near the handle.
 */
public class Door : Interactable
{
    public GameObject DoorObject;
    public GameObject Frame;

    public float OpenAngle = 90f;
    private bool _open = false;
    public float OpenTime = 1;

    private Collider _doorCol;
    
    private void Start()
    {
        Collider[] cols = DoorObject.GetComponents<Collider>();
        if (cols != null)
        {
            _doorCol = cols.First(collider1 => !collider1.isTrigger);
        }
    }

    protected override void OnInteract(PlayerController player)
    {
        Vector3 a = player.transform.position - Frame.transform.position;
        float sideOfDoorway = Vector3.Dot(-a.normalized, Frame.transform.right);
        float sideOfDoor = Vector3.Dot(a.normalized, DoorObject.transform.right);

        float rot;
        int dir;
        if (_open)
        {
            dir = -1;
            if (sideOfDoorway > 0)
            {
                rot = OpenAngle;
            }
            else
            {
                rot = -OpenAngle;
            }
        }
        else
        {
            dir = 1;
            if (sideOfDoorway > 0)
            {
                rot = -OpenAngle;
            }
            else
            {
                rot = OpenAngle;
            }
        }

        StartCoroutine(OpenAnimation(rot, dir, OpenTime));

        _open = !_open;
    }

    IEnumerator OpenAnimation(float rot, int dir, float openTime)
    {
        if (_doorCol)
        {
            _doorCol.enabled = false;
        }
        int steps = (int) (Mathf.Abs(rot) * 2);
        float waitTime = openTime / steps;
        float delta = rot / steps;
        while (steps > 0)
        {
            DoorObject.transform.Rotate(Vector3.up, dir * delta, Space.World);

            yield return new WaitForSeconds(waitTime);
            --steps;
        }
        if (_doorCol)
        {
            _doorCol.enabled = true;
        }
    }
}