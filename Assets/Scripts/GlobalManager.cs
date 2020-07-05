using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public List<Texture2D> textureList = new List<Texture2D>();
    public List<string> textureAddrList = new List<string>();
    private bool isLoadingOver = false;
    
    private void Start()
    {
        DontDestroyOnLoad(this);

        // 读取设置文件
        InitSetting();

        // 如果进行图片展示，则读取图片目录
        if (Define.IsShowcaseOn) 
        { 
            StartCoroutine(GetPicList());
        }
        else
        {
            isLoadingOver = true;
        }
        // 待实装 ：读取bgm目录

    }

    void Update()
    {
        if (isLoadingOver)
        {
            // 为避免延时，从下一秒开始时才进行场景跳转
            float waitTime = (float)(DateTime.UtcNow.Ticks * 0.0001 % 1000);
            waitTime = 1f - waitTime * 0.001f;
            isLoadingOver = false;
            Invoke("LoadLevel", waitTime);
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("PlayScene");
    }

    private IEnumerator GetPicList()
    {
        string dataPath = Application.dataPath + "/Resources/pic/";

        DirectoryInfo root = new DirectoryInfo(dataPath);
        FileInfo[] files = root.GetFiles();

        foreach (var file in files)
        {
            string[] splitStr = file.Name.Split('.');
            if (splitStr[splitStr.Length - 1] != "png" && splitStr[splitStr.Length - 1] != "jpg") // todo : 其他格式图片后缀判定?
            {
                continue;
            }
            
            textureAddrList.Add(file.FullName);
        }

        if (Define.IsPreloadOn)
        {
            foreach (var fileName in textureAddrList)
            {
                using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(fileName))
                {
                    yield return www.SendWebRequest();
                    if (www.isNetworkError)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        Texture2D myTexture = DownloadHandlerTexture.GetContent(www);
                        textureList.Add(myTexture);
                    }
                }
            }
        }
        else
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureAddrList[UnityEngine.Random.Range(0, textureAddrList.Count)]))
            {
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Texture2D myTexture = DownloadHandlerTexture.GetContent(www);
                    textureList.Add(myTexture);
                }
            }
        }

        isLoadingOver = true;
        yield return null;
    }

    private void InitSetting()
    {
        string filePathName = Application.dataPath + "/Resources/Setting.json";

        // 如果设置文件不存在，则进行创建
        if (!File.Exists(filePathName))
        {
            StreamWriter sw = new StreamWriter(filePathName);
            sw.Write(new Setting().SaveToString());
            sw.Close();
        }

        // 读取并应用设置
        StreamReader srSettingFile = new StreamReader(filePathName);
        string strSetting = System.Text.RegularExpressions.Regex.Replace(srSettingFile.ReadToEnd(), "[\r\n\t]", "");
        Setting.CreateFromJSON(strSetting).ApplySetting();
    }

}
