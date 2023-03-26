using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public AudioSource[] pullSounds;
    // Start is called before the first frame update
    void Start()
    {
        pullSounds[Random.Range(0, pullSounds.Length)].Play();
    }
}
