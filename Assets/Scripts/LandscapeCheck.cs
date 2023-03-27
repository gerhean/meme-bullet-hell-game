using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeCheck : MonoBehaviour
{
    public GameObject toShow;

    // Update is called once per frame
    void Update()
    {
        if (Screen.width > Screen.height) {
            toShow.SetActive(false);
            // Debug.Log("Landscape");
        } else {
            toShow.SetActive(true);
            // Debug.Log("Portrait");
        }
    }
}
