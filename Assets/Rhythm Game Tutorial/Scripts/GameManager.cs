using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource audioSource;

    bool isPlaying = false;

    public static float beginTime = 0;

    private void Update() {
        if (Input.anyKeyDown && !isPlaying) {
            audioSource.Play();
            isPlaying = true;
            beginTime = Time.time;
            //GameEvents.StartGame?.Invoke();
        }
    }
}
