using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public static Bubble instance;
    private bool _isPlaying;
    private bool _isEndStage;
    [SerializeField] private float _maxRadius;

    private Animator _animator;


    void Awake()
    {
        _isPlaying = false;
        _isEndStage = false;
        _animator = GetComponent<Animator>();

        instance = this;
    }

    private void OnEnable()
    {
        GameEvents.SetBuddleRadius += GameEvents_SetBuddleRadius;
        GameEvents.SetBubbleState += GameEvents_SetBubbleState;
        GameEvents.EndGame += GameEvents_EndGame;

    }
    private void OnDisble()
    {
        GameEvents.SetBuddleRadius -= GameEvents_SetBuddleRadius;
        GameEvents.SetBubbleState -= GameEvents_SetBubbleState;
        GameEvents.EndGame -= GameEvents_EndGame;


    }

    private void GameEvents_EndGame()
    {
        _isEndStage = true;
    }

    private void GameEvents_SetBubbleState(BubbleStateType type)
    {
        switch (type)
        {
            case (BubbleStateType.Purple):
                _animator.Play("ToPurple");
                Debug.Log("ToPurple");
                break;
            case (BubbleStateType.Boom):
                _animator.Play("Boom");
                break;
            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameEvents_StartGame()
    {
        _isPlaying = true;
    }

    void GameEvents_SetBuddleRadius(float radius)
    {
        float newRadius = radius;

        if (_isEndStage)
        {

        }
        else
        {
            if (radius >= _maxRadius)
            {
                newRadius = _maxRadius;
            }
            if (radius <= 1)
            {
                newRadius = 1;
            }
        }

        transform.localScale = new Vector3(newRadius, newRadius, newRadius);
    }

}
