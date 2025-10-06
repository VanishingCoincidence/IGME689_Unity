using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas timer;
    [SerializeField] Canvas winScreen;
    [SerializeField] Canvas loseScreen;
    public TMP_Text timerText;

    public bool win;
    public bool timerOn;
    public float timeLeft;

    [SerializeField] AudioManager audioManager;

    void Start()
    {
        timer.enabled = true;
        winScreen.enabled = false;
        loseScreen.enabled = false;
        this.GetComponent<Collider>().enabled = true;

        timerOn = true;
    }

    void Update()
    {
        if(timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimer(timeLeft);
            }
            else
            {
                timeLeft = 0;
                timerOn = false;
                win = false;
                GameOver();
            }
            
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            win = true;
            GameOver();
        }

    }

    void UpdateTimer(float currentTime)
    {
        // add to the timer
        currentTime++;

        // figure otu which is minutes and which is seconds
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        // updated timer UI
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    void GameOver()
    {
        if (win)
        {
            timerOn = false;
            timer.enabled = false;
            winScreen.enabled = true;
            audioManager.Play("winSound");
        }
        else
        {
            timerOn = false;
            timer.enabled = false;
            loseScreen.enabled = true;
            audioManager.Play("loseSound");
            this.GetComponent<Collider>().enabled = false;
        }
    }
    
}
