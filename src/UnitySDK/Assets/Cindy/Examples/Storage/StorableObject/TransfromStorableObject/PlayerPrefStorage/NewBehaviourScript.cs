using Cindy;
using Cindy.Util.Serializables;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public AsyncOperation x { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(test2());
    }

    IEnumerator test()
    {
        string url = "https://www.dustlight.cn/";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SendWebRequest();

            yield return webRequest.SendWebRequest();

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.LogError(webRequest.error + "\n" + webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
            }
            
        }
    }

    IEnumerator test2()
    {
        AsyncOperation a = SceneManager.LoadSceneAsync("FreeLook");
        a.allowSceneActivation = false;
        x = a;
        yield return a;
    }

    // Update is called once per frame
    void LateUpdate()
    {if (x != null)
        {
            Debug.Log(x.progress);
            if (x.progress == 0.9f)
                x.allowSceneActivation = true;
        }
    

    }
}
