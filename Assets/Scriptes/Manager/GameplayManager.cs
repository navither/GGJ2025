using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _bubble;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _cloud;

    [SerializeField] private GameObject _canvasObj;
    private Canvas _canvas;

    [SerializeField] private float _expansionTime;
    [SerializeField] private float _expansionRadius;


    private float _bubbleRadius;

    private bool _canMainCharacterMove;
    private bool _canChangeColor;
    private bool _isChangedColor;
    private bool _isPlaying;
    private bool _disableChangeColor;

    public static bool started = false;

    private void Awake()
    {
        _canMainCharacterMove = false;
        _isPlaying = false;
        _disableChangeColor = false;
        _canvas = _canvasObj.GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        GameEvents.EndGame += GameEvents_EndGame;
        GameEvents.StartGame += GameEvents_StartGame;
        GameEvents.PreStartGameTwoStage += GameEvents_PreStartGameTwoStage;
    }

    private void OnDisble()
    {
        GameEvents.EndGame -= GameEvents_EndGame;
        GameEvents.StartGame -= GameEvents_StartGame;

        GameEvents.PreStartGameTwoStage -= GameEvents_PreStartGameTwoStage;

    }

    private void GameEvents_StartGame()
    {
        _isPlaying = true;

        started = true;
    }

    private void GameEvents_PreStartGameTwoStage()
    {
        _canMainCharacterMove = true;
        _canChangeColor = false;
        _isChangedColor = false;
    }

    private void GameEvents_EndGame()
    {
        _isPlaying = false;
        _disableChangeColor = true;

        _bubbleRadius = _bubble.transform.localScale.y;
        StartCoroutine(EndGame());
    }

    private void Update()
    {
        if (_canMainCharacterMove)
        {
            if (Vector3.Distance(_bubble.transform.position, Vector3.zero) < 0.01)
            {
                _bubble.transform.position = Vector3.zero;
                _canMainCharacterMove = false;
                GameEvents.StartGame?.Invoke();
            }
            else
            {
                _bubble.transform.position += new Vector3(0, 5, 0) * Time.deltaTime;
                _character.transform.position += new Vector3(0, 5, 0) * Time.deltaTime;
            }
        }


        if (_isPlaying && !_disableChangeColor)
        {
            if (!_isChangedColor)
            {
                if (_cloud.transform.position.y < 0)
                {
                    _canChangeColor = true;
                    _isChangedColor = true;
                }
            }

            if (_canChangeColor)
            {
                GameEvents.SetBubbleState(BubbleStateType.Purple);

                _canChangeColor = false;
            }

        }


    }

    IEnumerator EndGame()
    {
        float time = 0;


        while (time < _expansionTime)
        {
            time += Time.deltaTime;
            float t = time / _expansionTime;
            float bubbleExpansionRadius = _expansionRadius / _circle.transform.localScale.y;
            float newScale = Mathf.Lerp(_bubbleRadius, bubbleExpansionRadius, t);
            _bubble.transform.localScale = Vector3.one * newScale;
            yield return null; // �ȴ���һ֡
        }
        _canvas.sortingOrder = 3;
        UIEvents.OpenEndView();

        GameEvents.SetBubbleState(BubbleStateType.Boom);

        while (time < _expansionTime + 7f)
        {
            time += Time.deltaTime;
            float t = time / _expansionTime;
            float bubbleExpansionRadius = _expansionRadius / _circle.transform.localScale.y;
            float newScale = _bubbleRadius + t * (bubbleExpansionRadius - _bubbleRadius);
            _bubble.transform.localScale = Vector3.one * newScale;
            yield return null; // �ȴ���һ֡
        }

    }


}
