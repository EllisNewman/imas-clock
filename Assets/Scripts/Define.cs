using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    //设置选项
    public static bool IsMusicOn;
    public static bool IsClockSoundOn;
    public static bool IsShowcaseOn;
    public static bool IsPreloadOn;
    public static float MusicVolume;
    public static float ClockVolume;

    // 固定位置以进行自适应屏幕尺寸的动画
    public static Vector3 PositionCenter = new Vector3(0f, 0f, 0f);
    public static Vector3 PositionUp     = new Vector3(0f, Screen.height, 0f);
    public static Vector3 PositionDown   = new Vector3(0f, -1 * Screen.height, 0f);
    public static Vector3 PositionLeft   = new Vector3(-1 * Screen.width, 0f, 0f);
    public static Vector3 PositionRight  = new Vector3(Screen.width, 0f, 0f);

}
