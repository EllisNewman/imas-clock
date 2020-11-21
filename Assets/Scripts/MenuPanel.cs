using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MenuPanel : MonoBehaviour
{
    public Button btnClose;
    public GameObject commonPanel;
    public GameObject colorPanel;
    public GameObject aboutPanel;

    public Toggle tglCommon;
    public Toggle tglColor;
    public Toggle tglAbout;

    public Text tglCommonText;
    public Text tglColorText;
    public Text tglAboutText;

    public void Start()
    {
        tglCommon.Select();
    }

    public void OnToggle()
    {
        commonPanel.SetActive(false);
        colorPanel.SetActive(false);
        aboutPanel.SetActive(false);
        if (tglCommon.isOn)
        {
            commonPanel.SetActive(true);
            tglCommonText.color = Color.white;
            tglColorText.color = Color.black;
            tglAboutText.color = Color.black;
            tglCommonText.fontSize = 18;
            tglColorText.fontSize = 14;
            tglAboutText.fontSize = 14;
        }
        if (tglColor.isOn)
        {
            colorPanel.SetActive(true);
            tglCommonText.color = Color.black;
            tglColorText.color = Color.white;
            tglAboutText.color = Color.black;
            tglCommonText.fontSize = 14;
            tglColorText.fontSize = 18;
            tglAboutText.fontSize = 14;
        }
        if (tglAbout.isOn)
        {
            aboutPanel.SetActive(true);
            tglCommonText.color = Color.black;
            tglColorText.color = Color.black;
            tglAboutText.color = Color.white;
            tglCommonText.fontSize = 14;
            tglColorText.fontSize = 14;
            tglAboutText.fontSize = 18;
        }
    }

    public void OnBtnCloseClick()
    {
        // 保存设置
        string filePathName = Application.dataPath + "/Resources/Setting.json";
        StreamWriter sw = new StreamWriter(filePathName);
        sw.Write(new Setting(true).SaveToString());
        sw.Close();

        gameObject.SetActive(false);
    }
}
