using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    void Awake()
    {
        if(textMesh == null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        textMesh.SetText(DateTime.Now.ToLongTimeString().Replace(":",""));
    }
}
