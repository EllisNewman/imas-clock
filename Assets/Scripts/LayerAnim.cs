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
    public GameObject TitleObject;

    public bool isShowcaseSetOn = true;

    private RectTransform trans;
    private float posX;
    private float posY;
    private float screenX;
    private float screenY;

    private delegate void delegateAnim();
    private delegateAnim[] delegateAnimList = new delegateAnim[4];
    private string clockStr;
    private int listCounter = -1;
    private int secondCounter = -1;
    private bool isShowcasing = false;

    void Start()
    {
        // 将前景图片的尺寸设为当前屏幕高和宽
        screenX = Screen.width;
        screenY = Screen.height;
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(screenX, screenY);
        trans = GetComponent<RectTransform>();
        trans.sizeDelta = new Vector2(screenX, screenY);

        delegateAnimList[0] = Anim1;
        delegateAnimList[1] = Anim2;
        delegateAnimList[2] = Anim3;
        delegateAnimList[3] = Anim4;

        clockStr = clockObject.GetParsedText();
    }

    private void LateUpdate()
    {
        // 1秒经过。判断条件为屏幕上文本变化
        // todo : 需对应暂停后继续时时间错位问题。判断条件改为clock click？
        if (clockStr != clockObject.GetParsedText())
        {
            if(listCounter == -1)
            {
                listCounter++;
                clockStr = clockObject.GetParsedText();
                StartCoroutine(setTitleAcvitity());
                return;
            }

            delegateAnimList[listCounter < 0 ? 0 : listCounter]();

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

    private IEnumerator setTitleAcvitity()
    {
        yield return new WaitForSeconds(1.9f);
        TitleObject.SetActive(false);
    }

    private void Anim1()
    {
        transform.localPosition = Define.PositionDown;
        transform.DOLocalMove(Define.PositionCenter, 0.5f);
    }
    private void Anim2()
    {
        transform.localPosition = Define.PositionCenter;
        transform.DOLocalMove(Define.PositionRight, 0.5f);
    }
    private void Anim3()
    {
        transform.localPosition = Define.PositionUp;
        transform.DOLocalMove(Define.PositionCenter, 0.5f);
    }
    private void Anim4()
    {
        transform.localPosition = Define.PositionCenter;
        transform.DOLocalMove(Define.PositionLeft, 0.5f);
    }
}
