using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageItem : MonoBehaviour
{
    public Button btnImage;
    public GameObject chosenImage;
    public Color currentColor;
    public bool isChosen = false;
    public int id;

    void Start()
    {
        btnImage.onClick.AddListener(OnBtnImageClick);
    }

    public void OnBtnImageClick()
    {
        if(isChosen)
        {
            isChosen = false;
        }
        else
        {
            isChosen = true;
        }
    }
}
