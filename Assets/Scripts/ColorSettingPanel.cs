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
    public GameObject multiColorImageItem;
    public GameObject colorGroupContent;

    public Toggle tglColorChange;

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
    }

    public void OnBtnEditColor()
    {
        if (currentItemId == -1) return;

    }

    public void OnBtnDeleteColor()
    {
        if (currentItemId == -1) return;

        colorList.RemoveAt(currentItemId);

        ImageItem[] itemList = colorGroupContent.GetComponentsInChildren<ImageItem>();
        onChoseItem -= itemList[currentItemId].OnChooseAt;

        Debug.LogWarning("Delete At: " + currentItemId);
        Destroy(itemList[currentItemId].gameObject);
        currentItemId = -1;
    }

    public void OnItemChoosed(int id)
    {
        currentItemId = id;
        onChoseItem(id);
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
