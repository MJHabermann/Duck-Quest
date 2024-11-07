using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public string audioType;
    void ArrowHit(){
        if(audioType == "ArrowHit"){
            audioSource.Play();
            Debug.Log("Playing " + audioType);
        }
    }

    void ArrowLoose(){
        if(audioType == "ArrowLoose"){
            audioSource.Play();
            Debug.Log("Playing " + audioType);
        }
    }
}