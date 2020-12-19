using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private DeathAudio death;
    private CharacterController controller;
    private MenuAudio audio;
    private float jumpHeight = 20.0f;
    private float gravity = 1.0f;
    private float yVelocity = 0.0f;
    public float score, bestScore;
    public bool isGameOver = false;
    public bool powerup = false;
    private bool isCoroutineReady = true;
    public bool isAdDeath = true;

    public Material[] color;

    public float countdownTimer = 3;
    Vector3 offSet;

    private UIManager uimanager;
    [SerializeField]
    private GameObject start, runParticle, overParticle;

    [SerializeField]
    float currentSpeed = 10.0f;
    float maxSpeed = 25f;
    public float accelerationTime = 60;    
    private float minSpeed;
    private float time;

    private Renderer colorMat;
    
    // Start is called before the first frame update
    void Start()
    {   
        audio = GameObject.Find("AudioManager").GetComponent<MenuAudio>();
        colorMat = GetComponent<Renderer>();
        offSet = new Vector3(0, 1, -3);
        death = GameObject.Find("DeathSound").GetComponent<DeathAudio>();
        bestScore = Mathf.Round(PlayerPrefs.GetFloat("BestScore", 0));
        runParticle.SetActive(true);
        overParticle.SetActive(false);
        Time.timeScale = 1;
        controller = GetComponent<CharacterController>();
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        minSpeed = currentSpeed; 
        time = 0;

        Camera.main.transform.parent = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer -= Time.deltaTime;
        uimanager.countdownDisplay(countdownTimer);
        if (countdownTimer < 0)
        {
            runParticle.transform.position = transform.position;
            overParticle.transform.position = transform.position;
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, (time / accelerationTime) / 4);
            time += Time.deltaTime;
            score = Vector3.Distance(start.transform.position, transform.position);
            uimanager.scoreDisplay(score);
            jump();
            gameController();
            checkForBestScore(); 

            if (powerup)
            {
                changeColor(powerup);
                if (isCoroutineReady)
                {
                    isCoroutineReady = false;
                    StartCoroutine(powerupCountDown());
                    isAdDeath = false;
                }
            }

            else
            {
                changeColor(powerup);
            }
        }
    }

    public void checkForBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("BestScore", bestScore);
        }
    }

    void jump()
    {
        if (controller.isGrounded && !isGameOver)
        {   
            if (Input.touchCount > 0)
            {
                yVelocity = jumpHeight;
            }          
        }

        else if(controller.isGrounded == false && !isGameOver)
        {
            yVelocity -= gravity;
        }
    }

    void gameController()
    {
        
        if (!isGameOver)
        {
            float dirX = Input.acceleration.x;
            runParticle.SetActive(true);
            Camera.main.transform.parent = this.transform;
            Vector3 direction = new Vector3(Mathf.Clamp(dirX, -2.0f, 2.0f), 0, 1);
            Vector3 velocity = currentSpeed * direction;
            this.gameObject.SetActive(true);
            velocity.y = yVelocity;
            controller.Move(velocity * Time.deltaTime);
            
            if (transform.position.y < -3)
            {
                isGameOver = true;
                overParticle.SetActive(true);
                if (isAdDeath)
                {
                    setScoreUIActive();
                    this.gameObject.SetActive(true);
                    transform.position = new Vector3(0, 1, transform.position.z);
                    uimanager.advertisementUI.SetActive(false);
                    isGameOver = false;
                    isAdDeath = false;
                }
                checkForBestScore();   
                death.play();  
            }

            else
            {
                uimanager.advertisementUI.SetActive(false);
            }
        }

        else
        {
            audio.pause();
            runParticle.SetActive(false);
            death.play();
            uimanager.gameOver(score);
            Camera.main.transform.parent = null;
            checkForBestScore(); 
            this.gameObject.SetActive(false);   
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if (hit.transform.tag == "Obstacle")
        { 
            if (!powerup)
            {
                if (!isGameOver)
                {
                    isGameOver = true;  
                    overParticle.SetActive(true); 
                    StartCoroutine(gameOverDelay());
                }
            }
        }
    }

    void changeColor(bool change)
    {
        if (change)
        {
            colorMat.sharedMaterial = color[1];
        }

        else
        {
            colorMat.sharedMaterial = color[0];
        }
    }

    public void unPauseMusic()
    {
        audio.resume();
    }
    
    void switchBool()
    {
        powerup = false;
    }

    public void setScoreUIActive()
    {
        uimanager.scoreUI.SetActive(true);
    }

    IEnumerator gameOverDelay()
    {
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0;
    }

    IEnumerator powerupCountDown()
    {
        yield return new WaitForSeconds(10.0f);
        switchBool();     
        isCoroutineReady = true;
        yield return null;
    }
}