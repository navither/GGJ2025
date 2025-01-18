using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _startViewPrefab;
    [SerializeField] GameObject _gameplayViewPrefab;
    [SerializeField] GameObject _endViewPrefab;
    [SerializeField] Transform _canvas;

    private void OnEnable()
    {
        UIEvents.OpenStartView += UIEvents_OpenStartView;
        UIEvents.OpenGameplayView += UIEvents_OpenGameplayView;
    }

    private void OnDisble()
    {
        UIEvents.OpenStartView -= UIEvents_OpenStartView;
        UIEvents.OpenGameplayView -= UIEvents_OpenGameplayView;
    }


    private void UIEvents_OpenStartView()
    {
        Instantiate(_startViewPrefab, _canvas);
    }

    private void UIEvents_OpenGameplayView()
    {
        Instantiate(_gameplayViewPrefab, _canvas);
    }




}
