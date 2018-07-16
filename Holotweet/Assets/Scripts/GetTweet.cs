using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Twity.DataModels.Responses;

public class GetTweet : MonoBehaviour {
    public string[] tweets;
    // Use this for initialization
    void Start()
    {
        tweets = new string[4];

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["count"] = 30.ToString();
        parameters["screen_name"] = "radi_bow";
        StartCoroutine(Twity.Client.Get("statuses/user_timeline", parameters, Callback));
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
                    if(j % 10 == 9)
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
}

