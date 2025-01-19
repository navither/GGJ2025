using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("主要的背景音乐，从游戏开始播放")]
    public AudioSource audioSource;

    public GameObject noteHolder;
    public GameObject ring;

    bool isPlaying = false;

    bool gameOver = false;

    public static float beginTime = 0;

    private void Update() {
        if (Input.anyKeyDown && !isPlaying) {
            audioSource.Play();
            isPlaying = true;
            beginTime = Time.time;
            GameEvents.StartGame?.Invoke();
        }

        if(audioSource.isPlaying == false && isPlaying && !gameOver) {
            GameEvents.EndGame?.Invoke();
            noteHolder.SetActive(false);
            ring.SetActive(false);
            gameOver = true;
        }
    }
}
