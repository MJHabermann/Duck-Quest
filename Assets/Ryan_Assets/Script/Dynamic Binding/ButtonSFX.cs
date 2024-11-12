using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    // Assignable sound effects for the button; these are set in the Inspector.
    public SoundEffect whooshSoundEffect;
    public SoundEffect clickSoundEffect;

    // Set up listeners for button click events
    void Start()
    {
        // Attach sound methods to button click event
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
        GetComponent<Button>().onClick.AddListener(PlayWhooshSound);
    }

    // Ensure sound effect objects persist across scenes
    void Awake()
    {
        DontDestroyOnLoad(clickSoundEffect.gameObject);
        DontDestroyOnLoad(whooshSoundEffect.gameObject);
    }

    // Plays the assigned click sound effect (if assigned)
    public void PlayClickSound()
    {
        if (clickSoundEffect != null)
        {
            clickSoundEffect.Play();
            Debug.Log("Click sound played");
        }
    }

    // Plays the assigned whoosh sound effect (if assigned)
    public void PlayWhooshSound()
    {
        if (whooshSoundEffect != null)
        {
            whooshSoundEffect.Play();
            Debug.Log("Whoosh sound played");
        }
    }
}
