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
    Purple,
    Boom
}

public static class GameEvents
{
    public static Action StartGame;

    public static Action<float> SetBuddleRadius;
    public static Action<float> SetBuddleRotateSpeed;

    public static Action<float> SetBackgroundMoveSpeed;

    public static Action<CharacterStateType> SetCharacterState;
    public static Action<BubbleStateType> SetBubbleState;

    //Instantiate的都是复制场景中的物体然后SetActive的
    public static Action InstantiateBirdImpact;
    public static Action<Vector3> InstantiateBird;


    public static Action EndGame;

    public static Action PreStartGame;
    public static Action PreStartGameTwoStage;

}
