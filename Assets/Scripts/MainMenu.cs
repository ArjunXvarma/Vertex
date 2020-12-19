using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Text bestScore;
    [SerializeField]
    private GameObject bestScoreObj, playButtonObj, quitButtonObj, muteBtn, unMuteBtn;
    Resolution[] resolutions;
    int currentResolutionIndex = 0;
    public bool songPlaying = true;
    MenuAudio audio;

    void Start() 
    {
        
        muteBtn.SetActive(true);
        unMuteBtn.SetActive(false);
        audio = GameObject.Find("AudioManager").GetComponent<MenuAudio>();
        bestScore.text = "BEST: " + Mathf.Round(PlayerPrefs.GetFloat("BestScore", 0)).ToString();
        resolutions = Screen.resolutions;
        audio.play();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
            }
        }
    }

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void mute()
    {
        audio.stop();
        muteBtn.SetActive(false);
        unMuteBtn.SetActive(true);
    }

    public void unMute()
    {
        audio.play();
        muteBtn.SetActive(true);
        unMuteBtn.SetActive(false);
    }
}
