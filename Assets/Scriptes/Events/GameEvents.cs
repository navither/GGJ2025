using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStateType
{ 
    Idle,
    Blow
}
public enum BubbleStateType
{
    Purple
}

public static class GameEvents
{
    public static Action StartGame;

    public static Action<float> SetBuddleRadius;
    public static Action<float> SetBuddleRotateSpeed;

    public static Action<float> SetBackgroundMoveSpeed;

    public static Action Restart;

    public static Action<CharacterStateType> SetCharacterState;
    public static Action<BubbleStateType> SetBubbleState;

}
