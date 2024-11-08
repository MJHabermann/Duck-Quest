using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhooshSound : SoundEffect
{
    public AudioSource whoosh;

    //public void Play()
    public override void Play()
    {
        if(whoosh != null)
        {
            whoosh.Play();
            Debug.Log("Playing whoosh sound");
        }
    }
}
