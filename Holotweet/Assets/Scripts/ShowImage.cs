using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShowImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("GetImage");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GetImage()
    {
        GetTweet getTweet = GameObject.Find("GetTweet").GetComponent<GetTweet>();
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(getTweet.url);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            this.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }
}
