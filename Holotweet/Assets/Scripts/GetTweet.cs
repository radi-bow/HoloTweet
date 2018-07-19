using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twity.DataModels.Responses;

public class GetTweet : MonoBehaviour {
    public string[] tweets;
    public string url;
    // Use this for initialization
    void Start()
    {
        tweets = new string[4];
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Callback(bool success, string response)
    {
        if (success)
        {
            StatusesHomeTimelineResponse Response = JsonUtility.FromJson<StatusesHomeTimelineResponse>(response);
            for(int i = 0;i< 4; i++)
            {
                string rawString = Response.items[i].text;
                string tweet = "";
                for(int j = 0;j< rawString.Length; j++)
                {
                    tweet += rawString[j];
                    if(j % 12 == 11)
                    {
                        tweet += "\n";
                    }
                }
                tweets[i] = tweet;
            }
        }
        else
        {
            Debug.Log(response);
        }
    }

    public void SearchTweet(string s)
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["count"] = 4.ToString();
        parameters["screen_name"] = s;
        StartCoroutine(Twity.Client.Get("statuses/user_timeline", parameters, Callback));
    }

    void ImageCallback(bool success, string response)
    {
        if (success)
        {
            StatusesHomeTimelineResponse Response = JsonUtility.FromJson<StatusesHomeTimelineResponse>(response);
            url = Response.items[0].user.profile_image_url;
            Debug.Log(url);
        }
        else
        {
            Debug.Log(response);
        }
    }

    public void GetAccountImage(string s)
    {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["count"] = 1.ToString();
        parameters["screen_name"] = s;
        StartCoroutine(Twity.Client.Get("statuses/user_timeline", parameters, ImageCallback));
    }
}

