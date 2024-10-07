using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    public AudioSource click;
    public AudioSource whoosh;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    // Update is called once per frame
    void PlaySound()
    {
        click.Play();
        whoosh.Play();
    }

    void Awake()
    {
        DontDestroyOnLoad(click);
        DontDestroyOnLoad(whoosh);
    }
}
