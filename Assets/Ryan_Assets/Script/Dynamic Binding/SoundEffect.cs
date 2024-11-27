using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// Plays the sound effect. This method is marked as `virtual` to allow 
    /// derived classes to override it with their own specific sound behavior.
    /// This enables dynamic binding, where the appropriate version of `Play`
    /// is determined at runtime based on the object type.
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
