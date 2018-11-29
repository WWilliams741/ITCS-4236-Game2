using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    private Text timerText, speedText, countdownText;
    private static int seconds, minutes, countdown;
    [SerializeField]
    private RollMovement playerScript;

    // Use this for initialization
    void Start()
    {
        //initialize seconds and minutes to zero on start
        if(SceneManager.GetActiveScene().name == "MichaelLevel(RockWork)")
        {
            seconds = 0;
            minutes = 0;
            countdown = 6;
            //start timer
            //StartCoroutine(TimeTracker());
            StartCoroutine(RaceStartCountdown());
        }
    }

    //countdown to start of race
    IEnumerator RaceStartCountdown()
    {
        bool countingDown = true;
        while (countingDown)
        {
            yield return new WaitForSeconds(1);
            countdown--;
            if (countdown == 0)
            {
                countingDown = false;
            }
        }

        GameManager.raceIsStarting = false;
        StartCoroutine(TimeTracker());
    }

    //time tracker coroutine
    IEnumerator TimeTracker()
    {
        //continue to add a second every second
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "EndScreen")
        {
            minutes += 0;
            seconds += 0;
        }
        //if reached 60 seconds
        if (seconds == 60)
        {
            //add a minute
            minutes += 1;
            //reset seconds
            seconds = 0;
        }

        //display the timer
        timerText.text = "Time: " + minutes + ":" + seconds.ToString("00"); ;

        //display current speed
        speedText.text = "" + Mathf.Floor(playerScript.GetCurrentSpeed() * 1.5f) + "mph";

        //display countdown text
        if (countdown == 3)
        {
            countdownText.color = Color.red;
            countdownText.text = "Ready!";
        }
        else if (countdown == 2)
        {
            countdownText.color = Color.yellow;
            countdownText.text = "Set!";
        }
        else if (countdown == 1)
        {
            countdownText.color = Color.green;
            countdownText.text = "Go!";
        }
        else
        {
            countdownText.text = "";
        }
    }
}
