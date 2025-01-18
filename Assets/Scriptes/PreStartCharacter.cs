using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreStartCharacter : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    [SerializeField] private GameObject targetObj;
    [SerializeField] private GameObject EndObj;

    [SerializeField] private GameObject bubble;

    private bool isPreStart;
    private bool isAttach;

    public void Awake()
    {
        isPreStart = false;
        isAttach = false;
    }
    private void OnEnable()
    {
        GameEvents.PreStartGame += GameEvents_PreStartGame;
    }


    private void OnDisble()
    {
        GameEvents.PreStartGame -= GameEvents_PreStartGame;
    }

    private void GameEvents_PreStartGame()
    {
        isPreStart = true;
    }

    public void Update()
    {
        if (isPreStart)
        {
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetObj.transform.position) < 0.1f)
            {
                isAttach = true;
            }

            if (isAttach)
            {
                bubble.transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
            }

            if (Vector3.Distance(transform.position, EndObj.transform.position) < 0.1f)
            {
                Camera.main.gameObject.transform.position = new Vector3(0, 0, -10);
                Camera.main.orthographicSize = 18.9f;
                GameEvents.PreStartGameTwoStage?.Invoke();

                Destroy(gameObject);
            }

        }

    }
}
