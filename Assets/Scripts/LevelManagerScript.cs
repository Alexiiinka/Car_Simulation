using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    TMPro.TextMeshProUGUI timeT;
    TMPro.TextMeshProUGUI restartT;
    TMPro.TextMeshProUGUI speedT;
    

    [SerializeField] Color redColor;
    [SerializeField] float timeOnStart = 10.50f;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject crashedPanel;
    [SerializeField] GameObject endPanelSuccess;
    [SerializeField] GameObject endPanelNOTSucc;
    [SerializeField] TMPro.TextMeshProUGUI timeForSuccess;
    [SerializeField] TMPro.TextMeshProUGUI timePlayers;

    [SerializeField] AudioSource playersAudioMotor;

    [SerializeField] CarStats carStatsSc;

    

    public bool gameOn = true; // for PAUSE purpose
    public bool gameEnded = false; // for total end
    
    void Start()
    {
        Time.timeScale = 1.0f;
        timeT = GameObject.Find("t_Time").GetComponent<TMPro.TextMeshProUGUI>();
        restartT = GameObject.Find("t_Restart").GetComponent<TMPro.TextMeshProUGUI>();
        speedT = GameObject.Find("t_Speed").GetComponent<TMPro.TextMeshProUGUI>();
        timeForSuccess.text = $"Beat this time: {timeOnStart.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {   
        if (!gameEnded) //final end of game  (victory or car crashed fatal)
        {
            if (gameOn) //the game is not paused
            {
                CountingTime();
                SpeedOnScreen();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseGame();
            }
            if (carStatsSc.health == 0)
            {
                gameOn = false;
                gameEnded = true;
                crashedPanel.SetActive(true);
                Time.timeScale = 0.0f;
                playersAudioMotor.enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }

    void CountingTime()
    {
        if ((timeOnStart - Time.timeSinceLevelLoad) > 0.0f)
        {
            timeT.text = $"Time left: {(timeOnStart - Time.timeSinceLevelLoad).ToString("F2")}";
        }
        else
        {
            timeT.text = $"Time OUT: 00:00";
            timeT.faceColor = redColor;
            restartT.enabled = true;
        }
    }

    void PauseGame()
    {
        if (gameOn)
        {
            Time.timeScale = 0.0f;
            gameOn = false;
            pausePanel.SetActive(true);
            playersAudioMotor.enabled = false;
        }
        else
        {
            Time.timeScale = 1.0f;
            gameOn = true;
            pausePanel.SetActive(false);
            playersAudioMotor.enabled = true;
        }
    }

    void SpeedOnScreen()
    {
        if (carStatsSc.speed > 0)
        {
            speedT.text = $"KmH: {(carStatsSc.speed).ToString("F0")}";
        }
        else
        {
            speedT.text = $"KmH: 0";
        }
    }

    public void GameEndedFinish()
    {
        timePlayers.text = $"Your time: {Time.timeSinceLevelLoad}";
        if (timeOnStart - Time.timeSinceLevelLoad > 0) //success
        {
            gameEnded = true;
            gameOn = false;
            Time.timeScale = 0.0f;
            endPanelSuccess.SetActive(true);
        }
        else //didn't finish in time
        {
            gameEnded = true;
            gameOn = false;
            Time.timeScale = 0.0f;
            endPanelNOTSucc.SetActive(true);
        }
        
    }
}
