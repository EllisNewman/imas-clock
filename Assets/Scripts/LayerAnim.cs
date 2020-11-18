using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class LayerAnim : MonoBehaviour
{
    public GameObject imageAnchor;
    public GameObject imageObject;
    public GameObject textClockObject;
    public GameObject textLocObject;
    public Showcase showcase;
    public Color currentColor;

    private RectTransform rectTransform;
    private delegate void delegateAnim();
    private int listCounter = 0;
    private int secondCounter = 0;
    private bool isShowcasing = false;

    // 设置选项
    public bool isOptionShowcaseOn = true;
    private List<Color> colorList;
    private int colorChangeCounter = 0;
    public  int colorIndex = 0;
    private int colorChangeFreq = 60;
    private string colorChangeMode = "";

    void Start()
    {
        // 获取设置选项
        isOptionShowcaseOn = Define.IsShowcaseOn;
        colorChangeMode = Define.ColorChangeMode;
        if (int.TryParse(Define.ColorChangeFreq, out colorChangeFreq))
        {
            colorChangeFreq -= 1;
        }
        else
        {
            colorChangeFreq = 59;
        }

        // 单色模式下的颜色设定，未取得时设置初始值
        if (Define.ColorSingleMode == "")
        {
            SetColor(new Color(0.078f, 0.952f, 1));
            Define.SetColorSingle(new Color(0.078f, 0.952f, 1));
        }

        // 多背景色模式的背景色列表取得，未取得时设置初始值
        colorList = Define.GetColorList();
        if(colorList == null || colorList.Count == 0)
        {
            colorList.Add(new Color(0.078f, 0.952f, 1));
            colorList.Add(new Color(0.498f, 0.537f, 1));
            colorList.Add(new Color(0.530f, 0.976f, 0.58f));
            colorList.Add(new Color(1, 0.631f, 0.647f));
        }
        Define.SetColorList(colorList);

        // 设置选项：单色模式
        if (!Define.IsColorChange)
        {
            SetColor(Define.GetColorSingle());
        }
        // 设置选项：多色模式
        else
        {
            SetColor(colorList[0]);
        }
        
        // 将前景图片的尺寸设为当前屏幕高和宽
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        currentColor = imageObject.GetComponent<Image>().color;

    }

    // 移动图片时反向移动文字，以保持文字居于画面中央
    // 由于移动图片的动画在Update中执行，因此本操作放在LateUpdate中随后执行
    private void LateUpdate()
    {
        float posX = rectTransform.localPosition.x;
        float posY = rectTransform.localPosition.y;
        imageAnchor.transform.localPosition = new Vector3(posX * -1f, posY * -1f, imageAnchor.transform.position.z);
    }

    // 每当1秒经过时被调用，执行每秒的动画切页和图片展示
    public void ClockClick()
    {
        switch (listCounter)
        {
            case 0: AnimDownToCenter();  break;
            case 1: AnimCenterToRight(); break;
            case 2: AnimTopToCenter();   break;
            case 3: AnimCenterToLeft();  break;
        }

        listCounter += 1;

        if (listCounter >= 4)
        {
            listCounter = 0;
        }

        // 设置选项：[图片展示]启用时
        if (isOptionShowcaseOn)
        {
            secondCounter++;

            if (secondCounter > 5)
            {
                secondCounter = 1;
                isShowcasing = !isShowcasing;
                if (isShowcasing)
                {
                    showcase.TriggerPicChange();

                    StartCoroutine(SetImageActivity(false));
                    showcase.transform.SetSiblingIndex(showcase.transform.GetSiblingIndex() + 1);
                }
                else
                {
                    imageObject.SetActive(true);
                    StartCoroutine(SetSibling());
                }
            }
        }
    }

    // 应用当前颜色至背景和文字
    public void SetColor(Color setColor)
    {
        currentColor = setColor;
        imageObject.GetComponent<Image>().color = currentColor;
        textClockObject.GetComponent<TextMeshProUGUI>().color = currentColor;
        textLocObject.GetComponent<Text>().color = currentColor;
    }

    public void ColorChangeClick()
    {
        if (!Define.IsColorChange) return;


        if (colorChangeCounter >= colorChangeFreq)
        {
            colorChangeCounter = 0;

            if ("random".Equals(colorChangeMode))
            {
                GetNextColorByRandom();
            }
            else
            {
                GetNextColorByOrder();
            }
        }
        else
        {
            colorChangeCounter += 1;
        }
    }

    #region 不用在意的实现细节

    private IEnumerator SetImageActivity(bool flag)
    {
        yield return new WaitForSeconds(0.9f);
        imageObject.SetActive(flag);
    }
    private IEnumerator SetSibling()
    {
        yield return new WaitForSeconds(1f);
        showcase.transform.SetAsFirstSibling();
    }

    private void AnimDownToCenter()
    {
        transform.localPosition = Define.PositionDown;
        transform.DOLocalMove(Define.PositionCenter, 0.5f);
    }
    private void AnimCenterToRight()
    {
        transform.localPosition = Define.PositionCenter;
        transform.DOLocalMove(Define.PositionRight, 0.5f);
    }
    private void AnimTopToCenter()
    {
        transform.localPosition = Define.PositionUp;
        transform.DOLocalMove(Define.PositionCenter, 0.5f);
    }
    private void AnimCenterToLeft()
    {
        transform.localPosition = Define.PositionCenter;
        transform.DOLocalMove(Define.PositionLeft, 0.5f);
    }

    public void GetNextColorByRandom()
    {
        int colorIndex = Random.Range(0, colorList.Count);

        SetColor(colorList[colorIndex]);
    }

    public void GetNextColorByOrder()
    {
        colorIndex += 1;
        if (colorIndex >= colorList.Count)
        {
            colorIndex = 0;
        }
        SetColor(colorList[colorIndex]);
    }
    #endregion
}
