using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{


    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameEvents.SetBackgroundMoveSpeed(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {

            GameEvents.SetBuddleRotateSpeed(300);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {

            GameEvents.SetBuddleRadius(2);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {

            GameEvents.SetBuddleRadius(3);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameEvents.SetCharacterState(CharacterStateType.Idle);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GameEvents.SetCharacterState(CharacterStateType.Blow);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameEvents.SetBubbleState(BubbleStateType.Purple);
        }
    }
}
