using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalManager : MonoBehaviour
{
    public List<Texture2D> textureList = new List<Texture2D>();
    private string dataPath;
    private bool isLoadingOver = false;
    
    private void Start()
    {
        DontDestroyOnLoad(this);

        // todo : 读取设置文件

        // 读取图片目录，如果设置为预加载则读取文件
        StartCoroutine(GetPicList());

        // todo : 读取bgm目录，如果设置为预加载则读取文件

    }

    void Update()
    {
        if (isLoadingOver)
        {
            // 为避免延时，从下一秒开始时才进行跳转
            float waitTime = (float)(DateTime.UtcNow.Ticks * 0.0001 % 1000);
            waitTime = 1f - waitTime * 0.001f;
            isLoadingOver = false;
            Invoke("LoadLevel", waitTime);
        }   
    }

    private IEnumerator GetPicList()
    {
        dataPath = Application.dataPath + "/Resources/pic/";

        DirectoryInfo root = new DirectoryInfo(dataPath);
        FileInfo[] files = root.GetFiles();

        foreach (var file in files)
        {
            string[] splitStr = file.Name.Split('.');
            if (splitStr[splitStr.Length - 1] != "png" && splitStr[splitStr.Length - 1] != "jpg") // todo : 其他格式图片后缀判定?
            {
                continue;
            }

            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(file.FullName))
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

    private void LoadLevel()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
