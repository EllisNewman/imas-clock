using System.Collections;
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
    public Color color;

    // 设置选项
    public bool isOptionShowcaseOn = true;

    private RectTransform rectTransform;
    private delegate void delegateAnim();
    private delegateAnim[] delegateAnimList = new delegateAnim[4];
    private int listCounter = 0;
    private int secondCounter = 0;
    private bool isShowcasing = false;

    void Start()
    {
        // 将前景图片的尺寸设为当前屏幕高和宽
        imageObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

        // 将动画序列协程存入委托列表中，以便按照索引依次播放
        delegateAnimList[0] = AnimDownToCenter;
        delegateAnimList[1] = AnimCenterToRight;
        delegateAnimList[2] = AnimTopToCenter;
        delegateAnimList[3] = AnimCenterToLeft;

        isOptionShowcaseOn = Define.IsShowcaseOn;
    }


    // 移动图片时反向移动文字，以保持文字居于画面中央
    // 由于移动图片的动画在Update中执行，因此本操作放在LateUpdate中随后执行
    private void LateUpdate()
    {
        float posX = rectTransform.localPosition.x;
        float posY = rectTransform.localPosition.y;
        imageAnchor.transform.localPosition = new Vector3(posX * -1f, posY * -1f, imageAnchor.transform.position.z);
    }

    public void ClockClick()
    {
        delegateAnimList[listCounter < 0 ? 0 : listCounter]();

        listCounter++;
        if (listCounter == 4)
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

    public void SetColor(Color setColor)
    {
        imageObject.GetComponent<Image>().color = color;
        textClockObject.GetComponent<TextMeshProUGUI>().color = color;
        textLocObject.GetComponent<Text>().color = color;
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
    #endregion
}
