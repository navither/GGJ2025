using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    [SerializeField] GameObject _bird;
    [SerializeField] GameObject _impact;

    private void OnEnable()
    {
        GameEvents.InstantiateBirdImpact += GameEvents_InstantiateBirdImpact;
        GameEvents.InstantiateBird += GameEvents_InstantiateBird;
    }

    private void OnDisable()
    {
        GameEvents.InstantiateBirdImpact -= GameEvents_InstantiateBirdImpact;
        GameEvents.InstantiateBird -= GameEvents_InstantiateBird;

    }



    private void GameEvents_InstantiateBirdImpact()
    {
        GameObject impact = Instantiate(_impact, _impact.transform.position, Quaternion.identity);
        impact.SetActive(true);

        Destroy(impact, 1);
    }

    private void GameEvents_InstantiateBird(Vector3 position)
    {
        GameObject bird = Instantiate(_bird, position, Quaternion.identity);
        bird.SetActive(true);
        Destroy(bird, 5);
    }

}
