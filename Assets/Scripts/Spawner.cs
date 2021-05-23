using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An interactable door in the game.
 * The player can open and close the door based on where they stand in relation to the hinge.
 * Requires that the door object pivot around its hinge for the desired effect.
 *
 * Place the Interactible's trigger near the handle.
 */
public class Spawner : Interactable
{
    //public GameObject Dispenser;
    //public GameObject SpawnedItem;
    public List<GameObject> prefabList = new List<GameObject>();

    /*
    protected override void OnInteract(PlayerController player)
    {
        Debug.Log("Interaction is working");
    }
    */

    public void Spawn()
    {
        int prefabIndex = UnityEngine.Random.Range(0, prefabList.Count - 1);
        Vector3 start = transform.position;
        start += transform.forward.normalized;
        GameObject a = (GameObject)Instantiate(prefabList[prefabIndex], start, transform.rotation);
        a.GetComponent<Rigidbody>().velocity =  transform.forward * 10;

    }
}