using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{


    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("KeyCode.M");
            GameEvents.SetBackgroundMoveSpeed(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("KeyCode.R");

            GameEvents.SetBuddleRotateSpeed(300);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("KeyCode.T");

            GameEvents.SetBuddleRadius(2);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("KeyCode.T");

            GameEvents.SetBuddleRadius(3);
        }

    }
}
