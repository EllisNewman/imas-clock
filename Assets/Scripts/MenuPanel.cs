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
        }
        if (tglColor.isOn)
        {
            colorPanel.SetActive(true);
        }
        if (tglAbout.isOn)
        {
            aboutPanel.SetActive(true);
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
