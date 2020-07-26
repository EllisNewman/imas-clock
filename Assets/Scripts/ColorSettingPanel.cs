using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSettingPanel : MonoBehaviour
{
    public LayerAnim layerAnim;

    public GameObject multiColorPanel;
    public GameObject singleColorPanel;
    public GameObject editColorWindow;

    public Toggle tglColorChange;

    // 单色模式
    public Button btnEditSingleColor;
    public Image imgSingleColor;

    // 多色模式
    public List<Color> colorList;

    void Start()
    {
        imgSingleColor.color = layerAnim.currentColor;

        colorList = Define.GetColorList();
        if(colorList.Count == 0)
        {
            colorList.Add(imgSingleColor.color);
        }


        if(Define.IsColorChange)
        {
            tglColorChange.isOn = true;
        }
        else
        {
            tglColorChange.isOn = false;
        }
        OnTglColorChange();
    }

    public void AddColor()
    {
        colorList.Add(new Color(0.078f, 0.952f, 1));
        colorList.Add(new Color(0.498f, 0.537f, 1));
        colorList.Add(new Color(0.530f, 0.976f, 0.58f));
        colorList.Add(new Color(1, 0.631f, 0.647f));
    }

    public void OnTglColorChange()
    {
        if(tglColorChange.isOn)
        {
            Define.IsColorChange = true;
            singleColorPanel.SetActive(false);
            multiColorPanel.SetActive(true);
        }
        else
        {
            Define.IsColorChange = false;
            singleColorPanel.SetActive(true);
            multiColorPanel.SetActive(false);
        }
    }

    #region 多背景色模式
    public void OnBtnAddColor()
    {

    }

    public void OnBtnEditColor()
    {

    }

    public void OnBtnDeleteColor()
    {

    }

    #endregion

    #region 单色模式
    public void OnBtnEditSingleColorClick()
    {
        ColorEditWindow colorEditWindow = Instantiate(editColorWindow, transform).GetComponent<ColorEditWindow>();
        colorEditWindow.setCurrentColor(imgSingleColor.color);
        colorEditWindow.OnWindowClose += OnReturnEditSingleColor;
    }

    public void OnReturnEditSingleColor(Color color)
    {
        // 将选定的颜色应用至UI和背景
        imgSingleColor.color = color;
        layerAnim.SetColor(color);

        // 保存至设置选项
        Define.SetColorSingle(color);
    }
    #endregion

}
