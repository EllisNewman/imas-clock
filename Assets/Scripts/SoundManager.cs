using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GameObject.Find("ClockClick").GetComponent<AudioSource>();
    }
    void Update()
    {
        Debug.Log("audio time: " + audioSource.time);
    }
}
