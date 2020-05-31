using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource clickSoundSource;

    void Start()
    {
        clickSoundSource = GameObject.Find("ClockClick").GetComponent<AudioSource>();
    }

    public void ClickStart()
    {
        if (!clickSoundSource.isPlaying)
        {
            clickSoundSource.Play();
        }
    }

    public void ClickCheck()
    {
        Debug.Log("audio time: " + (clickSoundSource.time + 0.16f) );
        long sec = DateTime.UtcNow.Ticks % 1000000000 / 10000;
        if (sec < 1000)
        {
            sec += 1000;
        }
        //Debug.LogError("audio sec " + sec);
    }
}
