using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class LayerAnim : MonoBehaviour
{
    public GameObject imageAnchor;
    public GameObject imageObject;
    public TextMeshProUGUI clockObject;
    public Showcase showcase;

    public bool isShowcaseSetOn = true;

    private RectTransform trans;
    private float posX;
    private float posY;
    private float screenX;
    private float screenY;

    private Vector3 positionCenter;
    private Vector3 positionUp;
    private Vector3 positionDown;
    private Vector3 positionLeft;
    private Vector3 positionRight;

    private string clockStr;
    private delegate void delegateAnim();
    private delegateAnim[] delegateAnimList = new delegateAnim[4];
    private int listCounter = 0;
    private int secondCounter = 0;

    private bool isShowcasing = false;
    private bool firstCall = true;
    void Start()
    {
        // 将前景图片的尺寸设为当前屏幕高和宽
        screenX = Screen.width;
        screenY = Screen.height;
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(screenX, screenY);
        trans = GetComponent<RectTransform>();
        trans.sizeDelta = new Vector2(screenX, screenY);

        // 初始化固定位置，以进行自适应屏幕尺寸的动画
        positionCenter = new Vector3(0f, 0f, 0f);
        positionUp     = new Vector3(0f,screenY, 0f);
        positionDown   = new Vector3(0f,-1 * screenY, 0f);
        positionLeft   = new Vector3(-1 * screenX, 0f, 0f);
        positionRight  = new Vector3(screenX, 0f, 0f);

        delegateAnimList[0] = Anim1;
        delegateAnimList[1] = Anim2;
        delegateAnimList[2] = Anim3;
        delegateAnimList[3] = Anim4;

        clockStr = clockObject.GetParsedText();
    }
    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        // 以文本变化为准的1秒经过
        if (clockStr != clockObject.GetParsedText())
        {
            if (firstCall)
            {
                firstCall = false;
                return;
            }

            delegateAnimList[listCounter < 0 ? 0 : listCounter]();

            Debug.Log((long)(System.DateTime.UtcNow.Ticks * 0.0001));

            listCounter++;
            if (listCounter == 4)
            {
                listCounter = 0;
            }

            // 设置选项：图片展示启用时
            if (isShowcaseSetOn)
            {
                secondCounter++;
                if (secondCounter >= 5)
                {
                    secondCounter = 0;

                    isShowcasing = !isShowcasing;
                    SetShowCase(isShowcasing);

                }
            }

            clockStr = clockObject.GetParsedText();
        }

        // 保持文字居于画面中央
        posX = trans.localPosition.x;
        posY = trans.localPosition.y;
        imageAnchor.transform.localPosition = new Vector3(posX * -1f, posY * -1f, imageAnchor.transform.position.z);
    }

    private void SetShowCase(bool isShowcasing)
    {
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

    private IEnumerator SetImageActivity(bool flag)
    {
        yield return new WaitForSeconds(1f);
        imageObject.SetActive(flag);
    }
    private IEnumerator SetSibling()
    {
        yield return new WaitForSeconds(1f);
        showcase.transform.SetAsFirstSibling();
    }

    private void Anim1()
    {
        transform.localPosition = positionCenter;
        transform.DOLocalMove(positionLeft, 0.5f);
    }
    private void Anim2()
    {
        transform.localPosition = positionDown;
        transform.DOLocalMove(positionCenter, 0.5f);
    }
    private void Anim3()
    {
        transform.localPosition = positionCenter;
        transform.DOLocalMove(positionRight, 0.5f);
    }
    private void Anim4()
    {
        transform.localPosition = positionUp;
        transform.DOLocalMove(positionCenter, 0.5f);
    }
}
