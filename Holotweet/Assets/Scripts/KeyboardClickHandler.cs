using System;
using HoloToolkit.UI.Keyboard;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
public class KeyboardClickHandler : MonoBehaviour, IInputClickHandler
{
    public TextMesh textMesh;

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
        textMesh.text = s;
    }
    private void KeyboardOnClosed(object sender, EventArgs eventArgs)
    {
        Keyboard.Instance.OnTextUpdated -= KeyboardOnTextUpdated;
        Keyboard.Instance.OnClosed -= KeyboardOnClosed;
    }
}