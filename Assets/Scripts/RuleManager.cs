using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class RuleManager : MonoBehaviour
{
    public TextMeshProUGUI clockObject;
    public LayerAnim layerAnim;
    public AudioManager audioManager;
    public Button BtnMenu;
    public MenuPanel menuPanel;

    public long milliSec;
    public float cacheTimer;

    void Start()
    {
        Debug.Log(DateTime.UtcNow.Ticks * 0.0001 % 1000);
    }


    void Update()
    {
        // millSec: 精确到毫秒的时间数值，1到10的秒数加上000到999的毫秒单位
        milliSec = DateTime.UtcNow.Ticks % 100000000 / 10000 + 1000;

        // 适应milliSec从一万回到一千的进位。
        if (milliSec < 2000 && cacheTimer > 9999)
        {
            cacheTimer = 0;
        }

        // 判断1秒经过
        if ((milliSec - cacheTimer) > 1000)
        {
            Debug.Log("TimeTic : " + DateTime.UtcNow.Ticks % 100000000);
            Debug.Log("millisec: " + milliSec);
            //Debug.Log("cacheTime   : " + cacheTimer);

            // 试图处理暂停导致的延时
            //if (Mathf.Abs(milliSec - cacheTimer) > 1500)
            //{
            //    audioManager.ClickReset();
            //    cacheTimer = milliSec / 1000 * 1000;
            //    Debug.LogError("Lag");
            //    return;
            //}

            cacheTimer = milliSec / 1000 * 1000;
            //Debug.LogWarning("cache sec " + cacheTimer);

            layerAnim.ClockClick();
            audioManager.ClickCheck();
        }
    }

    public void OnBtnMenuClick()
    {
        if(menuPanel.isActiveAndEnabled)
        {
            menuPanel.gameObject.SetActive(false);
        }
        else
        {
            menuPanel.gameObject.SetActive(true);
        }
    }
}
