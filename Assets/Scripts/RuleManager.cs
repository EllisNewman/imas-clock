using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RuleManager : MonoBehaviour
{
    public TextMeshProUGUI clockObject;
    public LayerAnim layerAnim;
    public AudioManager audioManager;

    private float cacheTimer;

    void Start()
    {
        Debug.Log(DateTime.UtcNow.Ticks * 0.0001 % 1000);
    }


    void Update()
    {
        long milliSec = DateTime.UtcNow.Ticks % 1000000000 / 10000;
        if(milliSec < 1000)
        {
            cacheTimer %= 1000;
        }

        // 1秒经过
        if ((milliSec - cacheTimer) > 1000)
        {
            Debug.Log(milliSec);
            Debug.Log(cacheTimer);
            // 试图处理暂停导致的延时
            if ((milliSec - cacheTimer) > 1100)
            {
                Debug.LogError("Do something");
                cacheTimer = milliSec;
                return;
            }

            audioManager.ClickStart();
            cacheTimer = milliSec / 1000 * 1000;
            Debug.Log("clock sec " + milliSec);

            layerAnim.ClockClick();
            audioManager.ClickCheck();
        }
    }
}
