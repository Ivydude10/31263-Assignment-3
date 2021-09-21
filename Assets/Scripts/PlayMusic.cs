using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip backgroundMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = 0.1f;
            audioSource.Play();
        }
    }
}
