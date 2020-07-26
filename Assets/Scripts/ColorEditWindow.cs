using UnityEngine;
using UnityEngine.UI;

public class ColorEditWindow : MonoBehaviour
{
    public Button btnConfirm;
    public Button btnCancel;
    public Image imgColor;
    public ColorPicker hsvpicker;

    public delegate void WindowClose(Color color);
    public WindowClose OnWindowClose;

    private Color prevColor;

    void Start()
    {
        btnConfirm.onClick.AddListener(OnBtnConfirm);
        btnCancel.onClick.AddListener(OnBtnCancel);
    }

    public void OnBtnConfirm()
    {
        OnWindowClose(imgColor.color);
        Destroy(gameObject);
    }

    public void OnBtnCancel()
    {
        OnWindowClose(prevColor);
        Destroy(gameObject);
    }

    public void setCurrentColor(Color color)
    {
        prevColor = color;
        hsvpicker.CurrentColor = color;
    }
}
