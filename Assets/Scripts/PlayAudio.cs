using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioSource audiosource;

    public bool allowInterrupt;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Let the audio finish before destroying the object.
    public void PlayAndDestroy()
    {
        GetComponent<Renderer>().enabled = false;
        PlaySound();
        Destroy(this, 0.5f);
    }

    public void PlaySound()
    {
        // Yeah I know it's bad...
        if(allowInterrupt)
            audiosource.Play();
        else if(!audiosource.isPlaying)
            audiosource.Play();
    }

    public void StopSound()
    {
        audiosource.Stop();
    }
}
