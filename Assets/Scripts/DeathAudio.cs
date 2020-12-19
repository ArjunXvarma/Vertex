using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAudio : MonoBehaviour
{
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void play()
    {
        audio.Play();
    }
}
