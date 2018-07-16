using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    public GazeManager gazeManager;
    public GameObject showKeyboard;

    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    // Use this for initialization
    void Start()
    {
        keywords.Add("Start", () =>
        {
            if (!GameObject.Find("ShowKeyboard(Clone)"))
            {
                Instantiate(showKeyboard, new Vector3(0, 0, 2.0f), Quaternion.identity);
            }
        });

        keywords.Add("Finish", () =>
        {
            var focusObject = gazeManager.HitObject;
            if (focusObject != null)
            {
                // Call the OnDrop method on just the focused object.
                if (focusObject.CompareTag("Account"))
                {
                    Destroy(focusObject.transform.root.gameObject);
                }
            }
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}