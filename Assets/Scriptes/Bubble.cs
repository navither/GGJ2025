using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public static Bubble instance;
    private bool _isPlaying;

    private Animator _animator;


    void Awake()
    {
        _isPlaying = false;
        _animator = GetComponent<Animator>();

        instance = this;
    }

    private void OnEnable()
    {
        GameEvents.SetBuddleRadius += GameEvents_SetBuddleRadius;
        GameEvents.SetBubbleState += GameEvents_SetBubbleState;
    }
    private void OnDisble()
    {
        GameEvents.SetBuddleRadius -= GameEvents_SetBuddleRadius;
        GameEvents.SetBubbleState -= GameEvents_SetBubbleState;


    }

    private void GameEvents_SetBubbleState(BubbleStateType type)
    {
        switch (type)
        {
            case (BubbleStateType.Purple):
                _animator.Play("ToPurple");
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
        transform.localScale = new Vector3(radius, radius, radius);
    }

}
