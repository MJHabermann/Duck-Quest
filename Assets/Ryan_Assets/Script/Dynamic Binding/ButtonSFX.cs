using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{

    public SoundEffect whooshSoundEffect;
    public SoundEffect clickSoundEffect;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
        GetComponent<Button>().onClick.AddListener(PlayWhooshSound);
    }

    void Awake()
    {
        DontDestroyOnLoad(clickSoundEffect.gameObject);
        DontDestroyOnLoad(whooshSoundEffect.gameObject);
    }

    // Update is called once per frame
    public void PlayClickSound()
    {
        if (clickSoundEffect != null)
        {
            clickSoundEffect.Play();
            Debug.Log("Click sound played");
        }
    }

    public void PlayWhooshSound()
    {
        if (whooshSoundEffect != null)
        {
            whooshSoundEffect.Play();
            Debug.Log("whoosh sound played");
        }
    }
}
