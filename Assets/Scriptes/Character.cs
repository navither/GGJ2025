using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject _bubble;

    private Transform _circle;
    private Transform _characterImage;

    private SpriteRenderer _circleSpriteRender;

    private float _originCircleRadius;
    private float _originBubbleScale;

    private float _originCharacterHalfY;
    private float _originCharacterScale;

    private Animator _animator;

    private void Awake()
    {
        _circle = _bubble.transform.Find("Circle");
        _circleSpriteRender = _circle.GetComponent<SpriteRenderer>();
        _originCircleRadius = _circleSpriteRender.sprite.bounds.size.y / 2 * _circle.localScale.y;
        _originBubbleScale = _bubble.transform.localScale.y;

        _characterImage = transform.Find("Image");
        _originCharacterHalfY = _characterImage.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2 * _characterImage.localScale.y;
        _originCharacterScale = transform.localScale.y;

        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameEvents.SetBuddleRadius += GameEvents_SetBuddleRadius;
        GameEvents.SetCharacterState += GameEvents_SetCharacterState;
    }
    private void OnDisble()
    {
        GameEvents.SetBuddleRadius -= GameEvents_SetBuddleRadius;
        GameEvents.SetCharacterState -= GameEvents_SetCharacterState;
    }

    private void GameEvents_SetCharacterState(CharacterStateType type)
    {
        switch (type) 
        {
            case (CharacterStateType.Idle):
                _animator.Play("Idle");
                break;
            case (CharacterStateType.Blow):
                _animator.Play("Blow");
                break;
            default:
                break;

        }
    }

    void GameEvents_SetBuddleRadius(float radius)
    {
        //transform.localScale = new Vector3(_originCircleScale)

        transform.localScale = Vector3.one * (_originBubbleScale / radius);
        transform.position = _bubble.transform.position - new Vector3(0, radius / _originBubbleScale * _originCircleRadius + _originBubbleScale / radius * _originCharacterHalfY * 0.9f, 0);

    }

}
