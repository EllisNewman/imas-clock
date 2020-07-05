using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Networking;

public class Showcase : MonoBehaviour
{
    public List<Texture2D> SpriteList = new List<Texture2D>();
    public List<string> SpriteAddrList = new List<string>();

    private RawImage image;
    private bool isPreloadOn;
    private bool isAsyncLoading = false;

    void Start()
    {
        image = GetComponentInChildren<RawImage>();
        image.rectTransform.sizeDelta = new Vector2(Screen.height * 16f / 9f, Screen.height);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        GlobalManager globalManager = GameObject.Find("GlobalManager")?.GetComponent<GlobalManager>();
        List<Texture2D> globalSpriteList = globalManager?.textureList;
        List<string> globalSpriteAddrList = globalManager?.textureAddrList;
        if (globalSpriteList != null && globalSpriteList.Count > 0)
        {
            SpriteList = globalSpriteList;
        }
        if (globalSpriteAddrList != null && globalSpriteAddrList.Count > 0)
        {
            SpriteAddrList = globalSpriteAddrList;
        }

        // 设置选项 ：是否预先加载图片资源
        isPreloadOn = Define.IsPreloadOn;
    }

    void Update()
    {
        if (!isPreloadOn)
        {
            if (SpriteList.Count < 3 && !isAsyncLoading)
            {
                StartCoroutine(AsyncLoad());
            }
        }   
    }

    public void TriggerPicChange()
    {
        // 设置选项 ：预先加载图片的场合，从图片列表中随机选取图片显示
        if (isPreloadOn)
        {
            if (SpriteList.Count != 0)
            {
                int index = Random.Range(0, SpriteList.Count);

                image.texture = SpriteList[index];
            }
        }
        // 设置选项 ：预先加载图片关闭的场合，选取异步加载的图片显示，随后将其清除
        else
        {
            if (SpriteList.Count != 0)
            {
                image.texture = SpriteList[0];
                SpriteList.RemoveAt(0);
            }
        }
        float ranX = Random.value < 0.5f ? -1f : 1f;
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = new Quaternion();

        // 设置选项：[图片展示动画]启用时
        if (Define.IsShowcaseAnimOn) { 
            transform.DORotate(new Vector3(0, 0, 2f * ranX), 6f);
            transform.DOScale(1.1f, 6f);
        }
    }

    private IEnumerator AsyncLoad()
    {
        isAsyncLoading = true;

        string filePathName = SpriteAddrList[Random.Range(0, SpriteAddrList.Count)];
        Debug.LogWarning("start async load at " + filePathName);
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(filePathName))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D myTexture = DownloadHandlerTexture.GetContent(www);
                SpriteList.Add(myTexture);
            }
        }

        Debug.LogWarning("end async load");
        isAsyncLoading = false;

        yield break;
    }
}
