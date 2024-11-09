using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    //public void Play()
    public virtual void Play() // Dynamically bound method
    {
        Debug.Log("Playing sound effect.");
    }

    public void ShowEffectName() // Statically bound method
    {
        Debug.Log("This is a generic sound effect.");
    }

}
