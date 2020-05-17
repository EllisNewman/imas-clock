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

    private RectTransform rectTransform;

    private delegate void delegateAnim();
    private delegateAnim[] delegateAnimList = new delegateAnim[4];
    private string clockStr;
    private int listCounter = 0;
    private int secondCounter = 0;
    private bool isShowcasing = false;

    private float cacheTimer;

    void Start()
    {
        long sec = DateTime.UtcNow.Ticks % 1000000000 / 10000;
        if (sec < 1000)
        {
            sec += 100000;
        }
        Debug.Log("first " + sec);
        // 将前景图片的尺寸设为当前屏幕高和宽
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        delegateAnimList[0] = Anim1;
        delegateAnimList[1] = Anim2;
        delegateAnimList[2] = Anim3;
        delegateAnimList[3] = Anim4;

        clockStr = clockObject.GetParsedText();
    }

    private void FixedUpdate()
    {
        long sec = DateTime.UtcNow.Ticks % 1000000000 / 10000;
        if (sec < 1000)
        {
            sec += 100000;
        }

        Debug.Log("clock sec " + sec);
        if ((sec / 1000) != cacheTimer)
        {
            cacheTimer = sec / 1000;
        }
    }

    private void LateUpdate()
    {
        // 1秒经过。判断条件为屏幕上文本变化
        // todo : 需对应暂停后继续时时间错位问题。判断条件改为clock click？
        if (clockStr != clockObject.GetParsedText())
        {
            //double waitTime = (DateTime.UtcNow.Ticks % 10000000 * 0.00001);
            //cacheWaitTime = waitTime;

            //Debug.Log("Tick is " + DateTime.UtcNow.Ticks);
            //Debug.Log("divided " + DateTime.UtcNow.Ticks % 10000000);
            //Debug.Log("plus " + DateTime.UtcNow.Ticks % 10000000 * 0.00001);


            //if (waitTime > 5)
            //{
            //    Debug.Log("do something to deal with lag");
            //}

            long sec = DateTime.UtcNow.Ticks % 1000000000 / 10000;
            if (sec < 1000)
            {
                sec += 100000;
            }
            Debug.LogError("text sec " + sec);

            delegateAnimList[listCounter < 0 ? 0 : listCounter]();

            listCounter++;
            if (listCounter == 4)
            {
                listCounter = 0;
            }

            // 设置选项：[图片展示]启用时
            if (isShowcaseSetOn)
            {
                secondCounter++;

                if (secondCounter > 5)
                {
                    secondCounter = 1;
                    isShowcasing = !isShowcasing;
                    SetShowCase(isShowcasing);
                }
            }

            clockStr = clockObject.GetParsedText();
        }

        // 保持文字居于画面中央
        float posX = rectTransform.localPosition.x;
        float posY = rectTransform.localPosition.y;
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
