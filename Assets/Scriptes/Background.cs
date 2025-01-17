using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _moveSpeed;

    private bool _isPlaying;



    void Awake()
    {
        _isPlaying = false;
    }

    private void OnEnable()
    {
        GameEvents.StartGame += GameEvents_StartGame;
        GameEvents.SetBackgroundMoveSpeed += GameEvents_SetBackgroundMoveSpeed;

    }
    private void OnDisble()
    {
        GameEvents.StartGame -= GameEvents_StartGame;
        GameEvents.SetBackgroundMoveSpeed -= GameEvents_SetBackgroundMoveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        //if (_isPlaying)
        //{
        //    transform.position -= new Vector3(0, _moveSpeed, 0) * Time.deltaTime;
        //}
        transform.position -= new Vector3(0, _moveSpeed, 0) * Time.deltaTime;

    }

    void GameEvents_StartGame()
    {
        _isPlaying = true;
    }

    void GameEvents_SetBackgroundMoveSpeed(float speed)
    {
        _moveSpeed = speed;
    }

}
