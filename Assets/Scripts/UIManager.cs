using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenuUI, pauseButton, gameOverUI, scoreUI, gameoveParticle, advertisementUI;
    [SerializeField]
    private GameObject gameOver_Text, scoreObj, countdownObj;
    [SerializeField]
    private Text scoreText, finalScore, countdownText, powerupCountDownText;
    private MenuAudio audio;
    private Player player;
    private int adCount = 1;
    private AdsManager ads;

    Resolution[] resolutions;
    int currentResolutionIndex = 0;

    void Start() 
    {
        ads = GameObject.Find("AdsManager").GetComponent<AdsManager>();
        audio = GameObject.Find("AudioManager").GetComponent<MenuAudio>();
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
            }
        }
        advertisementUI.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        scoreObj.SetActive(true);
        scoreUI.SetActive(true);
        audio.resume();
    }

    public void menu()
    {
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void gameOver(float score)
    {  
        
        StartCoroutine(gameOverText());
        pauseButton.SetActive(false);
        finalScore.text = "SCORE: " + Mathf.Round(score);
        scoreUI.SetActive(false);
        if(Application.internetReachability != NetworkReachability.NotReachable && ads.checkAds())
        {
            advertisementUI.SetActive(true);
            advertisementUI.transform.SetAsFirstSibling();
        } 
        else
        {
            advertisementUI.SetActive(false);
            StartCoroutine(gameoverDelay());
        }
    }

    public void restart()
    {

        SceneManager.LoadScene(1);
        audio.stop();
        audio.play();
    }

    public void scoreDisplay(float score)
    {
        float roundScore = Mathf.Round(score);
        scoreText.text = roundScore.ToString();
    }

    public void countdownDisplay(float seconds)
    {
        seconds = Mathf.Round(seconds);
        if (seconds > 0)
        {
            countdownText.text = seconds.ToString();
            pauseButton.SetActive(false);
            scoreObj.SetActive(false);
        }

        else
        {
            pauseButton.SetActive(true);
            scoreObj.SetActive(true);
            countdownObj.SetActive(false);
        } 
    }
    public void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
        pauseMenuUI.transform.SetAsLastSibling();
        scoreUI.SetActive(false);
        audio.pause();
    }

    public void no()
    {
        advertisementUI.SetActive(false);
        StartCoroutine(gameoverDelay());
    }

    public void yes()
    {
        advertisementUI.SetActive(false);
        ads.displayAds();
    }

    IEnumerator gameOverText()
    {
        while(true)
        {
            gameOver_Text.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameOver_Text.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator gameoverDelay()
    {
        yield return new WaitForSeconds(0.5f);
        gameOverUI.SetActive(true);
        gameOverUI.transform.SetAsLastSibling();
    }
}