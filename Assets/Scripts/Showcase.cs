using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Showcase : MonoBehaviour
{
    public List<Texture2D> SpriteList = new List<Texture2D>();

    private RawImage image;

    void Start()
    {
        image = GetComponentInChildren<RawImage>();
        image.rectTransform.sizeDelta = new Vector2(Screen.height * 16f / 9f, Screen.height);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);

        List<Texture2D> globalSpriteList = GameObject.Find("GlobalManager")?.GetComponent<GlobalManager>().textureList; // todo : REMOVE "?" BEFORE LAUNCH
        if(globalSpriteList != null && globalSpriteList.Count > 0)
        {
            SpriteList = globalSpriteList;
        }

        Debug.Log(SpriteList.Count + " of Sprites loaded.");
    }

    void Update()
    {
        
    }

    public void TriggerPicChange()
    {
        if(SpriteList.Count != 0)
        {
            int index = Random.Range(0, SpriteList.Count);

            image.texture = SpriteList[index];
        }
        float ranX = Random.value < 0.5f ? -1f : 1f;
        transform.localScale = new Vector3(1, 1, 1);
        transform.rotation = new Quaternion();

        transform.DORotate(new Vector3(0, 0, 2f * ranX), 6f);
        transform.DOScale(1.1f, 6f);
    }
}
