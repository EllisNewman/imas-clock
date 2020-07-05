using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSettingPanel : MonoBehaviour
{
    public List<Color> colorList = new List<Color>();
    public LayerAnim layerAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Apply()
    {
    }

    public void AddColor()
    {
        colorList.Add(new Color(0.078f, 0.952f, 1));
        colorList.Add(new Color(0.498f, 0.537f, 1));
        colorList.Add(new Color(0.530f, 0.976f, 1));
        colorList.Add(new Color(1, 0.631f, 0.647f));
    }
}
