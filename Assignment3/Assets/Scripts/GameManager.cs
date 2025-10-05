using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas timer;
    [SerializeField] Canvas winScreen;
    [SerializeField] Canvas loseScreen;
    public TMP_Text timerText;

    public bool win;

    private void Start()
    {
        timer.enabled = true;
        winScreen.enabled = false;
        loseScreen.enabled = false;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            win = true;
        }

    }

    private void GameOver()
    {
        if (win)
        {
            timer.enabled = false;
            winScreen.enabled = true;
        }
        else
        {
            timer.enabled = false;
            loseScreen.enabled = true;
        }
    }
    
}
