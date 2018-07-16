using System;
using HoloToolkit.UI.Keyboard;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
public class KeyboardClickHandler : MonoBehaviour, IInputClickHandler
{
    public TextMesh textMesh;
    public GetTweet getTweet;
    public GameObject virtualAccount;

    void Start()
    {
        getTweet = GameObject.Find("GetTweet").GetComponent<GetTweet>();
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Keyboard.Instance.Close();
        Keyboard.Instance.PresentKeyboard();
        Keyboard.Instance.RepositionKeyboard(transform, null, 0.1f);

        Keyboard.Instance.OnTextUpdated += KeyboardOnTextUpdated;
        Keyboard.Instance.OnClosed += KeyboardOnClosed;
    }
    private void KeyboardOnTextUpdated(string s)
    {
        textMesh.text = "@" + s;
        getTweet.SearchTweet(s);
    }
    private void KeyboardOnClosed(object sender, EventArgs eventArgs)
    {
        GameObject account = Instantiate(virtualAccount, this.transform.position, this.transform.rotation);
        account.transform.Find("Tweet1").GetComponent<TextMesh>().text = getTweet.tweets[0];
        account.transform.Find("Tweet2").GetComponent<TextMesh>().text = getTweet.tweets[1];
        account.transform.Find("Tweet3").GetComponent<TextMesh>().text = getTweet.tweets[2];
        account.transform.Find("Tweet4").GetComponent<TextMesh>().text = getTweet.tweets[3];
        //getTweet.SearchTweet(textMesh.text);
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(getTweet.tweets[i]);
        }
        Keyboard.Instance.OnTextUpdated -= KeyboardOnTextUpdated;
        Keyboard.Instance.OnClosed -= KeyboardOnClosed;
        Destroy(transform.root.gameObject);
    }
}