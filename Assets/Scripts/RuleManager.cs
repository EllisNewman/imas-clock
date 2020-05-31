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

    public long milliSec;
    public float cacheTimer;

    void Start()
    {
        Debug.Log(DateTime.UtcNow.Ticks * 0.0001 % 1000);
    }


    void Update()
    {
        //milliSec = DateTime.UtcNow.Ticks % 1000000000 / 10000;
        milliSec = DateTime.UtcNow.Ticks % 100000000 / 10000 + 1000;
        if (milliSec < 2000)
        {
            cacheTimer = 1000;
        }
        Debug.Log("milliTic : " + DateTime.UtcNow.Ticks % 100000000);
        Debug.Log("millisec : " + milliSec);
        Debug.Log("cacheTime: " + cacheTimer);

        // 1秒经过
        if (((milliSec - cacheTimer) > 1000))
        {

            // 试图处理暂停导致的延时
            if (Mathf.Abs(milliSec - cacheTimer) > 1500)
            {
                audioManager.ClickReset();
                cacheTimer = milliSec / 1000 * 1000;
                Debug.LogError("Lag");
                return;
            }

            cacheTimer = milliSec / 1000 * 1000;
            Debug.LogWarning("clock sec " + milliSec);
            Debug.LogWarning("cache sec " + cacheTimer);

            layerAnim.ClockClick();
            audioManager.ClickCheck();
        }
    }
}
