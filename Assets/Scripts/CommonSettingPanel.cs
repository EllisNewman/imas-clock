using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommonSettingPanel : MonoBehaviour
{
    public Toggle tglClockSound;
    public Toggle tglShowcase;
    public Toggle tglShowcaseAnim;
    public Toggle tglPreload;
    public Toggle tglNightMode;
    public Scrollbar scrClockSound;
    public AudioManager audioManager;
    public Image layoutImage;
    public TextMeshProUGUI layoutClockText;
    public TextMeshProUGUI layoutLocText;

    void Start()
    {
        tglClockSound.SetIsOnWithoutNotify(Define.IsClockSoundOn);
        tglShowcase.SetIsOnWithoutNotify(Define.IsShowcaseOn);
        tglShowcaseAnim.SetIsOnWithoutNotify(Define.IsShowcaseAnimOn);
        tglPreload.SetIsOnWithoutNotify(Define.IsPreloadOn);
        tglNightMode.SetIsOnWithoutNotify(Define.IsNightMode);
        scrClockSound.value = Define.ClockVolume;

        if (!tglClockSound.isOn)
        {
            scrClockSound.gameObject.SetActive(false);
        }
    }
    public void OnTglClockSoundChanged()
    {
        if (tglClockSound.isOn)
        {
            // 显示音量滑动条，并设置音量
            Define.IsClockSoundOn = true;
            scrClockSound.gameObject.SetActive(true);
            audioManager.SetTckSoundVolume(scrClockSound.value);
        }
        else
        {
            // 隐藏音量滑动条，并关闭声音
            Define.IsClockSoundOn = false;
            scrClockSound.gameObject.SetActive(false);
            audioManager.SetTckSoundVolume(0);
        }
    }

    public void OnScrClockSoundChanged()
    {
        Define.ClockVolume = scrClockSound.value;
        audioManager.SetTckSoundVolume(Define.IsClockSoundOn ? scrClockSound.value : 0);
    }

    public void OnTglShowcaseChanged()
    {
        if (tglShowcase.isOn)
        {
            Define.IsShowcaseOn = true;
        }
        else
        {
            Define.IsShowcaseOn = false;
        }
    }

    public void OnTglPreloadChanged()
    {
        if (tglPreload.isOn)
        {
            Define.IsPreloadOn = true;
        }
        else
        {
            Define.IsPreloadOn = false;
        }
    }

    public void OnTglShowcaseAnimChanged()
    {
        if (tglShowcaseAnim.isOn)
        {
            Define.IsShowcaseAnimOn = true;
        }
        else
        {
            Define.IsShowcaseAnimOn = false;
        }
    }

    public void OnTglNightModeChanged()
    {
        if (tglNightMode.isOn)
        {
            Define.IsNightMode = true;
            layoutImage.color = new Color(30 / 255f, 30 / 255f, 30 / 255f);
            layoutClockText.color = new Color(30 / 255f, 30 / 255f, 30 / 255f);
            layoutLocText.color = new Color(30 / 255f, 30 / 255f, 30 / 255f);
        }
        else
        {
            Define.IsNightMode = false;
            layoutImage.color = new Color(1,1,1);
            layoutClockText.color = new Color(1, 1, 1);
            layoutLocText.color = new Color(1, 1, 1);
        }
    }
}
