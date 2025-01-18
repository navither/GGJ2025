using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject _bubble;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _canvasObj;
    private Canvas _canvas;

    [SerializeField] private float _expansionTime;
    [SerializeField] private float _expansionRadius;

    private float _bubbleRadius;

    private void Awake()
    {
        _canvas = _canvasObj.GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        GameEvents.EndGame += GameEvents_EndGame;
    }

    private void OnDisble()
    {
        GameEvents.EndGame -= GameEvents_EndGame;
    }


    private void GameEvents_EndGame()
    {
        _bubbleRadius = _bubble.transform.localScale.y;
        StartCoroutine(EndGame());
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
            yield return null; // 等待下一帧
        }
        _canvas.sortingOrder = 3;
        UIEvents.OpenEndView();

        UIEvents.SetEndGameScore(11111);

        GameEvents.SetBubbleState(BubbleStateType.Boom);

        while (time < _expansionTime + 7f)
        {
            time += Time.deltaTime;
            float t = time / _expansionTime;
            float bubbleExpansionRadius = _expansionRadius / _circle.transform.localScale.y;
            float newScale = _bubbleRadius + t * (bubbleExpansionRadius - _bubbleRadius);
            _bubble.transform.localScale = Vector3.one * newScale;
            yield return null; // 等待下一帧
        }

    }


}
