using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColorSettingPanel : MonoBehaviour
{
    public LayerAnim layerAnim;

    public GameObject multiColorPanel;
    public GameObject singleColorPanel;
    public GameObject editColorWindow;
    public GameObject multiColorImageItem;
    public GameObject colorGroupContent;

    public Toggle tglColorChange;
    public Toggle tglColorChangeFreq5s;
    public Toggle tglColorChangeFreq1min;
    public Toggle tglColorChangeFreq10min;
    public Toggle tglColorChangeModOrder;
    public Toggle tglColorChangeModRandom;

    // 单色模式
    public Button btnEditSingleColor;
    public Image imgSingleColor;

    // 多色模式
    public List<Color> colorList;
    public int currentItemId = -1;
    private delegate void OnChoseItem(int id);
    private delegate void OnEditItem();
    private delegate void OnDeleteItem(int id);
    private OnChoseItem onChoseItem;
    private OnEditItem onEditItem;

    void Start()
    {
        // 单色模式的颜色初始值为当前背景色
        imgSingleColor.color = layerAnim.currentColor;

        // 读取多色模式颜色list并初始化颜色列表
        colorList = Define.GetColorList();
        if(colorList.Count == 0)
        {
            colorList.Add(imgSingleColor.color);
        }
        for(int i = 0; i < colorList.Count; i++)
        {
            ImageItem colorGroupItem = Instantiate(multiColorImageItem, colorGroupContent.gameObject.transform).GetComponent<ImageItem>();
            colorGroupItem.SetColor(colorList[i]);
            colorGroupItem.id = i;

            onChoseItem += colorGroupItem.OnChooseAt;
            colorGroupItem.onChooseEvent += OnItemChoosed;
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

    public void OnTglColorChange()
    {
        if(tglColorChange.isOn)
        {
            Define.IsColorChange = true;
            singleColorPanel.SetActive(false);
            multiColorPanel.SetActive(true);

            if ("5".Equals(Define.ColorChangeFreq))
            {
                tglColorChangeFreq5s.isOn = true;
            }
            else if ("60".Equals(Define.ColorChangeFreq))
            {
                tglColorChangeFreq1min.isOn = true;
            }
            else if ("600".Equals(Define.ColorChangeFreq))
            {
                tglColorChangeFreq10min.isOn = true;
            }

            if ("random".Equals(Define.ColorChangeMode))
            {
                tglColorChangeModRandom.isOn = true;
            }
            else
            {
                tglColorChangeModOrder.isOn = true;
            }
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
        ImageItem colorGroupItem = Instantiate(multiColorImageItem, colorGroupContent.gameObject.transform).GetComponent<ImageItem>();
        colorGroupItem.SetColor(Color.white);
        colorGroupItem.id = colorList.Count;
        colorList.Add(Color.white);

        onChoseItem += colorGroupItem.OnChooseAt;
        colorGroupItem.onChooseEvent += OnItemChoosed;

        // 保存设置
        Define.SetColorList(colorList);
    }

    public void OnBtnEditColor()
    {
        if (currentItemId == -1) return;

        List<ImageItem> itemList = colorGroupContent.GetComponentsInChildren<ImageItem>().ToList();
        ColorEditWindow colorEditWindow = Instantiate(editColorWindow, transform).GetComponent<ColorEditWindow>();
        colorEditWindow.setCurrentColor(itemList[currentItemId].currentColor);
        colorEditWindow.OnWindowClose += OnReturnEditingColorItem;
    }

    public void OnReturnEditingColorItem(Color color)
    {
        List<ImageItem> itemList = colorGroupContent.GetComponentsInChildren<ImageItem>().ToList();
        itemList[currentItemId].SetColor(color);
        colorList[currentItemId] = color;

        // 保存设置
        Define.SetColorList(colorList);
    }

    public void OnBtnDeleteColor()
    {
        if (currentItemId == -1) return;

        colorList.RemoveAt(currentItemId);

        // 取得颜色列表，删除目标颜色并刷新列表
        List<ImageItem> itemList = colorGroupContent.GetComponentsInChildren<ImageItem>().ToList();
        onChoseItem -= itemList[currentItemId].OnChooseAt;
        Destroy(itemList[currentItemId].gameObject);
        itemList.RemoveAt(currentItemId);
        currentItemId = -1;

        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].id = i;
        }

        // 保存设置
        Define.SetColorList(colorList);
    }

    public void OnItemChoosed(int id)
    {
        currentItemId = id;
        onChoseItem(id);
    }

    public void OnTglColorChangeFreq5s(bool value)
    {
        if (tglColorChangeFreq5s.isOn) 
        {
            Define.ColorChangeFreq = "5";
        }
    }

    public void OnTglColorChangeFreq1min(bool value)
    {
        if (tglColorChangeFreq1min.isOn)
        {
            Define.ColorChangeFreq = "60";
        }
    }

    public void OnTglColorChangeFreq10min(bool value)
    {
        if (tglColorChangeFreq10min.isOn)
        {
            Define.ColorChangeFreq = "600";
        }
    }

    public void OnTglColorChangeModOrder(bool value)
    {
        if (tglColorChangeModOrder.isOn)
        {
            Define.ColorChangeMode = "order";
        }
    }

    public void OnTglColorChangeModRandom(bool value)
    {
        if (tglColorChangeModRandom.isOn)
        {
            Define.ColorChangeMode = "random";
        }
    }

    #endregion

    #region 单色模式
    // 单色模式 编辑按钮
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
