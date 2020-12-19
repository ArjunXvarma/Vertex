using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public static MenuAudio instance;
    AudioSource audio;
    Player player;
    bool gameOver;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("Player is null");
        }
    }

    void Awake()
    {    
        if (instance == null)
        {
            instance = this;
            audio = GetComponent<AudioSource>();
            if (gameOver)
            {
                play();
            }
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void play()
    {
        audio.Play();
    }

    public void stop()
    {
        audio.Stop();
    }

    public void pause()
    {
        audio.Pause();
    }

    public void resume()
    {
        audio.UnPause();
    }
}
