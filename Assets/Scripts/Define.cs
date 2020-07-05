using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    //设置选项
    public static bool IsClockSoundOn = true;
    public static bool IsShowcaseOn = true;
    public static bool IsShowcaseAnimOn = true;
    public static bool IsPreloadOn = false;
    [Range(0f, 1f)]
    public static float ClockVolume = 0.5f;

    // 暂未实装
    public static float MusicVolume;
    public static bool IsMusicOn = false;

    // 固定位置以进行自适应屏幕尺寸的动画
    public static Vector3 PositionCenter = new Vector3(0f, 0f, 0f);
    public static Vector3 PositionUp     = new Vector3(0f, Screen.height, 0f);
    public static Vector3 PositionDown   = new Vector3(0f, -1 * Screen.height, 0f);
    public static Vector3 PositionLeft   = new Vector3(-1 * Screen.width, 0f, 0f);
    public static Vector3 PositionRight  = new Vector3(Screen.width, 0f, 0f);
}

[System.Serializable]
public class Setting
{
    public bool isClockSoundOn = true;
    public bool isShowcaseOn = true;
    public bool isSHowcaseAnimOn = true;
    public bool isPreloadOn = false;
    [Range(0f, 1f)]
    public float clockVolume = 0.5f;

    public Setting() { }

    public Setting(bool isSave) {
        if (isSave)
        {
            isClockSoundOn = Define.IsClockSoundOn;
            isPreloadOn = Define.IsPreloadOn;
            isShowcaseOn = Define.IsShowcaseOn;
            isSHowcaseAnimOn = Define.IsShowcaseAnimOn;
            clockVolume = Define.ClockVolume;
        }
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public static Setting CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Setting>(jsonString);
    }

    public void ApplySetting()
    {
        Define.IsClockSoundOn = isClockSoundOn;
        Define.IsPreloadOn = isPreloadOn;
        Define.IsShowcaseOn = isShowcaseOn;
        Define.IsShowcaseAnimOn = isSHowcaseAnimOn;
        Define.ClockVolume = clockVolume;
    }
}
