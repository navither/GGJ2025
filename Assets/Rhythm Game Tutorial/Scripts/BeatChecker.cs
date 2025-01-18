using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class BeatChecker : MonoBehaviour
{
    public static BeatChecker instance;

    [HideInInspector]
    public GameObject currentNote;

    public bool isTiming = false;

    public int score = 0;

    public int barScore = 0;

    public TextMeshProUGUI scoreText;

    public GameObject missEffect;

    public GameObject perfectEffect;

    public AudioClip[] clips;

    public AudioClip birdClip;

    AudioSource keyAudioSource;

    AudioSource birdAudioSource;

    private Dictionary<KeyCode, AudioClip> keySoundMap;

    private void OnEnable() {
        GameEvents.StartGame += PlayChorsClip;
    }

    private void OnDisable() {
        GameEvents.StartGame -= PlayChorsClip;
    }

    private void Start()
    {
        instance = this;

        keyAudioSource = GetComponent<AudioSource>();

        birdAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        birdAudioSource.clip = birdClip;
        birdAudioSource.mute = true;

        keySoundMap = new Dictionary<KeyCode, AudioClip>
        {
            { KeyCode.F, clips[0] },
            { KeyCode.J, clips[1] },
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.F))
        {
            if (isTiming)
            {
                //判定正确
                //Debug.Log("判定正确");

                score++;

                if(scoreText)
                scoreText.text = barScore.ToString();

                DOTween.Sequence().Append(perfectEffect.transform.DOScale(new Vector3(4, 4, 4), 0.3f).SetEase(Ease.OutBack))
                    .Append(perfectEffect.transform.DOScale(new Vector3(0, 0, 0), 0.2f));

                Destroy(currentNote);
            }
            else
            {
                DOTween.Sequence().Append(missEffect.transform.DOScale(new Vector3(4, 4, 4), 0.3f).SetEase(Ease.OutBack))
                    .Append(missEffect.transform.DOScale(new Vector3(0, 0, 0), 0.2f));
                //判定错误
                //Debug.Log("判定错误");
                //Vector2 originPos = missEffect.anchoredPosition;
                // DOTween.Sequence().Append(missEffect.DOAnchorPos(new Vector2(missEffect.anchoredPosition.x, 0), 0.6f).SetEase(Ease.InCubic))
                // .Append(missEffect.DOAnchorPos(new Vector2(missEffect.anchoredPosition.x, -250), 0.6f).SetEase(Ease.OutCubic))
                // .Append(missEffect.DOAnchorPos(originPos, 0));
            }

            foreach (var entry in keySoundMap)
            {
                if (Input.GetKeyDown(entry.Key))
                {
                    keyAudioSource.PlayOneShot(entry.Value);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space)){
                birdAudioSource.mute = false;
            }
        }
    }

    public void PlayChorsClip()
    {
        birdAudioSource.Play();
    }
}
