using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleScript : MonoBehaviour
{

    ParticleSystem particle;


    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        particle.Play();
        Destroy(particle);
    }

    // In the case of an exploding particle system, destroy exploding object
    public void MakeInvisible()
    {
        // Setting scale to 0 atm, because I can't get Destroy() to work
        this.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 0);
        //Destroy(this.transform.GetChild(0));
    }
}
