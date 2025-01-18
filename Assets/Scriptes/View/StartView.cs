using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartView : MonoBehaviour
{

    Button _startButton;
    Button _exitButton;

    void Awake()
    {
        _startButton = transform.Find("StartButton").GetComponent<Button>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();
    }

    private void OnEnable()
    {
        UIEvents.CloseStartView += UIEvents_CloseStartView;
        _startButton.onClick.AddListener(OnStartButton);
        _exitButton.onClick.AddListener(OnExitButton);
    }
    private void OnDisble()
    {
        UIEvents.CloseStartView -= UIEvents_CloseStartView;
    }

    private void UIEvents_CloseStartView()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnStartButton()
    {
        UIEvents.CloseStartView?.Invoke();
        UIEvents.OpenGameplayView?.Invoke();
    }

    private void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Update is called once per frame

}
