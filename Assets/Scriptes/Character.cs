using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject _bubble;


    private float _distance;
    private SpriteRenderer _bubbleSpriteRender;

    private float _originCircleRadius;
    private float _originCircleScale;
    private float _characterHalfY;

    private void Awake()
    {
        _bubbleSpriteRender = _bubble.transform.Find("Circle").GetComponent<SpriteRenderer>();
        _characterHalfY = GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        _originCircleRadius = _bubbleSpriteRender.sprite.bounds.size.y / 2;
        _originCircleScale = _bubble.transform.localScale.y;
    }

    private void OnEnable()
    {
        GameEvents.SetBuddleRadius += GameEvents_SetBuddleRadius;
    }
    private void OnDisble()
    {
        GameEvents.SetBuddleRadius -= GameEvents_SetBuddleRadius;

    }
    void GameEvents_SetBuddleRadius(float radius)
    {

        transform.position = _bubble.transform.position - new Vector3(0, radius / _originCircleScale * _originCircleRadius + _characterHalfY, 0);

    }

}
