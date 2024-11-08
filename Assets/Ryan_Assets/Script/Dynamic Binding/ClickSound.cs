using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : SoundEffect
{
    public AudioSource click;

    //public void Play()
    public override void Play()
    {
        if(click != null)
        {
            click.Play();
            Debug.Log("Playing click sound");
        }
    }
}
