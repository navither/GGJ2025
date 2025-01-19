using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("主要的背景音乐，从游戏开始播放")]
    public AudioSource audioSource;

    public AudioClip mainAudio;

    public GameObject noteHolder;
    public GameObject ring;

    bool isPlaying = false;

    bool gameOver = false;

    public static float beginTime = 0;

    float songLength = 0f;

    private void Start() {
        songLength = audioSource.clip.length;

        audioSource.Play();
    }

    private void Update() {
        if(GameplayManager.started == false)
            return;
        if (!isPlaying) {
            audioSource.clip = mainAudio;   
            audioSource.Play();
            isPlaying = true;
            beginTime = Time.time;
            //GameEvents.StartGame?.Invoke();
        }

        if(audioSource.isPlaying == false && Time.time - beginTime > songLength && !gameOver) {
            GameEvents.EndGame?.Invoke();
            noteHolder.SetActive(false);
            ring.SetActive(false);
            gameOver = true;
        }
    }
}
