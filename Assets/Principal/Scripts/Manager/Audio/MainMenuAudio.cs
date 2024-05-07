using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{

    public float fadeSpeed = 0.05f;
    private float targetVolume = 0.1f;

    public AudioSource audioSource;

    public MainMenuManager mainMenuManagerRef;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(mainMenuManagerRef.loadScene == true)
        {
            targetVolume = 0.010f;
            audioSource.volume -= fadeSpeed * Time.deltaTime;            
        }

        if (audioSource.volume <= targetVolume)
        {
            audioSource.volume = targetVolume;
        }
    }
}
