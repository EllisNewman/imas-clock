using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    //设置选项
    [Range(0f, 1f)]
    public static float ClockVolume = 0.5f;
    public static bool IsClockSoundOn = true;
    public static bool IsShowcaseOn = true;
    public static bool IsShowcaseAnimOn = true;
    public static bool IsPreloadOn = false;
    public static bool IsColorChange = false;
    public static string ColorChangeFreq;
    public static string ColorChangeMode;
    public static string ColorSingleMode;
    public static string[] ColorListArray;

    // 暂未实装
    public static float MusicVolume;
    public static bool IsMusicOn = false;

    // 设定位置，以进行自适应屏幕尺寸的动画
    public static Vector3 PositionCenter = new Vector3(0f, 0f, 0f);
    public static Vector3 PositionUp     = new Vector3(0f, Screen.height, 0f);
    public static Vector3 PositionDown   = new Vector3(0f, -1 * Screen.height, 0f);
    public static Vector3 PositionLeft   = new Vector3(-1 * Screen.width, 0f, 0f);
    public static Vector3 PositionRight  = new Vector3(Screen.width, 0f, 0f);

    public static void SetColorList(List<Color> colorList)
    {
        if(colorList.Count > 0)
        {
            ColorListArray = new string[colorList.Count];

            for(int i = 0; i < colorList.Count; i++)
            {
                ColorListArray[i] = GetColorStr(colorList[i]);
            }
        }
    }

    public static List<Color> GetColorList()
    {
        List<Color> colorList = new List<Color>();

        if(ColorListArray?.Length > 0)
        {
            for(int i = 0; i < ColorListArray.Length; i++)
            {
                colorList.Add(GetColorClass(ColorListArray[i]));
            }
        }

        return colorList;
    }

    public static void SetColorSingle(Color color)
    {
        ColorSingleMode = GetColorStr(color);
    }

    public static Color GetColorSingle()
    {
        return GetColorClass(ColorSingleMode);
    }

    private static string GetColorStr(Color color)
    {
        return color.r + "," + color.g + "," + color.b;
    }

    private static Color GetColorClass(string strColor)
    {
        string[] strRGB = strColor?.Split(',');
        if(strRGB == null || strRGB?.Length < 3)
        {
            return new Color();
        }

        return new Color(float.Parse(strRGB[0]),
                         float.Parse(strRGB[1]),
                         float.Parse(strRGB[2]));
    }
}

[System.Serializable]
public class Setting
{
    [Range(0f, 1f)]
    public float clockVolume = 0.5f;
    public bool isClockSoundOn = true;
    public bool isShowcaseOn = true;
    public bool isSHowcaseAnimOn = true;
    public bool isPreloadOn = false;
    public bool isColorChange = false;
    public string colorChangeFreq = "";
    public string colorChangeMode = "";
    public string colorSingleMode = "";
    public string[] colorListArray;

    public Setting(bool isSave = false) {
        if (isSave)
        {
            clockVolume = Define.ClockVolume;
            isClockSoundOn = Define.IsClockSoundOn;
            isPreloadOn = Define.IsPreloadOn;
            isShowcaseOn = Define.IsShowcaseOn;
            isSHowcaseAnimOn = Define.IsShowcaseAnimOn;
            isColorChange = Define.IsColorChange;
            colorChangeFreq = Define.ColorChangeFreq;
            colorChangeMode = Define.ColorChangeMode;
            colorSingleMode = Define.ColorSingleMode;
            colorListArray = Define.ColorListArray;
        }
    }

    public void ApplySetting()
    {
        Define.ClockVolume = clockVolume;
        Define.IsClockSoundOn = isClockSoundOn;
        Define.IsPreloadOn = isPreloadOn;
        Define.IsShowcaseOn = isShowcaseOn;
        Define.IsShowcaseAnimOn = isSHowcaseAnimOn;
        Define.IsColorChange = isColorChange;
        Define.ColorChangeFreq  = colorChangeFreq;
        Define.ColorChangeMode = colorChangeMode;
        Define.ColorSingleMode = colorSingleMode;
        Define.ColorListArray  = colorListArray;
    }

    // 保存设置
    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public static Setting CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Setting>(jsonString);
    }
}
