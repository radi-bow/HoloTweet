using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class GlobalClickHandler : MonoBehaviour, IInputClickHandler
{
    public GameObject showKeyboard;

    // Use this for initialization
    void Start () {
        InputManager.Instance.AddGlobalListener(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!GameObject.Find("ShowKeyboard(Clone)"))
        {
            Transform pos = GameObject.Find("Cursor").GetComponent<Transform>();
            Instantiate(showKeyboard, pos.position, pos.rotation * Quaternion.Euler(0, 180, 0));
        }
    }
    
}
