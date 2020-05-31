﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource clickSoundSource;

    private float clockCount;

    void Start()
    {
        clickSoundSource = GameObject.Find("ClockClick").GetComponent<AudioSource>();
    }

    public void ClickCheck()
    {
        if (!clickSoundSource.isPlaying)
        {
            clickSoundSource.Play();
        }

        if (Mathf.Abs(clockCount - clickSoundSource.time) > 0.3f)
        {
            Debug.LogError("Deal With Lag");
            Debug.LogError("Clock Count: " + clockCount);
            Debug.LogError("audio time: " + clickSoundSource.time);
            clickSoundSource.time = clockCount;
        }

        Debug.Log("Clock Count: " + clockCount);
        clockCount = (clockCount == 59) ? 0 : clockCount + 1;

        //long sec = DateTime.UtcNow.Ticks % 1000000000 / 10000;
        //if (sec < 1000)
        //{
        //    sec += 1000;
        //}
        //Debug.Log("audio sec " + sec);
    }

    public void ClickReset()
    {
        clickSoundSource.time = clockCount == 0 ? clickSoundSource.time : clockCount - 1;
    }
}
