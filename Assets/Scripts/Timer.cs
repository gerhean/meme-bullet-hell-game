using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textbox;
    public bool continueTimer = true;


    // Update is called once per frame
    void Update()
    {
        if (continueTimer)
            textbox.text = Time.timeSinceLevelLoad.ToString();
    }
}
