using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningDisc : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _rotationSpeed;

    private bool _isPlaying;



    void Awake()
    {
        _isPlaying = false;
    }

    private void OnEnable()
    {
        GameEvents.StartGame += GameEvents_StartGame;
        //GameEvents.SetBuddleRadius += GameEvents_SetBuddleRadius;
        GameEvents.SetBuddleRotateSpeed += GameEvents_SetBuddleRotateSpeed;
    }
    private void OnDisble()
    {
        GameEvents.StartGame -= GameEvents_StartGame;
        //GameEvents.SetBuddleRadius -= GameEvents_SetBuddleRadius;
        GameEvents.SetBuddleRotateSpeed -= GameEvents_SetBuddleRotateSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        //if (_isPlaying) 
        //{
        //    transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
        //}
        transform.Rotate(Vector3.forward, -_rotationSpeed * Time.deltaTime);

    }

    void GameEvents_StartGame()
    {
        _isPlaying = true;
    }

    void GameEvents_SetBuddleRadius(float radius)
    { 
        transform.localScale = new Vector3(radius, radius, radius);
    }

    void GameEvents_SetBuddleRotateSpeed(float speed)
    {
        _rotationSpeed = speed;
    }

}
