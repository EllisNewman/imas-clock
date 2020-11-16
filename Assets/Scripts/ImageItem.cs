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
    public delegate void OnChooseEvent(int id);
    public OnChooseEvent onChooseEvent;

    void Start()
    {
        btnImage.onClick.AddListener(OnBtnImageClick);
        chosenImage.SetActive(false);
    }

    public void OnBtnImageClick()
    {
        if (isChosen)
        {
            isChosen = false;
            chosenImage.SetActive(false);
            onChooseEvent(-1);
        }
        else
        {
            isChosen = true;
            chosenImage.SetActive(true);
            onChooseEvent(id);
        }
    }

    public void SetColor(Color color)
    {
        btnImage.image.color = color;
        currentColor = color;
    }

    public void OnChooseAt(int id)
    {
        if (this.id != id)
        {
            isChosen = false;
            chosenImage.SetActive(false);
        }
    }

}
