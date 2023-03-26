using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textbox;
    public bool continueTimer = true;
    public float timeRemaining = 300;


    // Update is called once per frame
    void Update()
    {
        if (timeRemaining <= 0) {
            timeRemaining = 0;
            continueTimer = false;
            textbox.text = "0 minute(s) 0 second(s)";
        }
        if (continueTimer) {
            timeRemaining -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            textbox.text = string.Format("{0:00} minute(s) {1:00} second(s)", minutes, seconds);
        }
    }
}
