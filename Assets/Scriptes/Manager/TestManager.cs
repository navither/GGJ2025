using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    float count = 1;

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            count += 1;

            float r = 1 + count * 0.05f;
            GameEvents.SetBuddleRadius(r);
        }

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            count -= 1;

            float r = 1 + count * 0.05f;
            GameEvents.SetBuddleRadius(r);
        }


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
            GameEvents.SetBuddleRadius(3);
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

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    GameEvents.SetBubbleState(BubbleStateType.Purple);
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameEvents.InstantiateBirdImpact();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            GameEvents.InstantiateBird(new Vector3(-18.2f, 9.6f, 0));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //GameEvents.SetBubbleState(BubbleStateType.Boom);
            GameEvents.EndGame();
        }
    }
}
