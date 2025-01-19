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

    public int barScore = 4;

    public TextMeshProUGUI scoreText;

    public GameObject missEffect;

    public GameObject perfectEffect;

    public AudioClip[] clips;

    public AudioClip birdClip;

    public AudioSource keyAudioSource;

    AudioSource birdAudioSource;

    AudioSource flyHintSource;

    public List<GameObject> birds;

    public GameObject flyingBird;

    int birdIndex = 0;

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

        flyHintSource = transform.GetChild(1).GetComponent<AudioSource>();

        keySoundMap = new Dictionary<KeyCode, AudioClip>
        {
            { KeyCode.F, clips[0] },
            { KeyCode.J, clips[1] },
            { KeyCode.D, clips[2] },
            { KeyCode.K, clips[3] },
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(GameplayManager.started == false)
            return;
        if(scoreText)
            scoreText.text = barScore.ToString();
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.K))
        {
            if (isTiming)
            {
                //判定正确
                //Debug.Log("判定正确");

                score++;

                if(score == 2){
                    //FlyBird();
                    flyingBird.SetActive(true);
                    flyHintSource.Play();
                    // DOTween.Sequence().AppendInterval(0.3f).AppendCallback(() => {
                    //     flyHintSource.Play();
                    // });
                }
    

                DOTween.Sequence().Append(perfectEffect.transform.DOScale(new Vector3(4, 4, 4), 0.3f).SetEase(Ease.OutBack))
                    .Append(perfectEffect.transform.DOScale(new Vector3(0, 0, 0), 0.2f));

                Destroy(currentNote);

                foreach (var entry in keySoundMap)
                {
                    if (Input.GetKeyDown(entry.Key))
                    {
                        keyAudioSource.PlayOneShot(entry.Value);
                    }
                }
            }
            else
            {
                Miss();
                //判定错误
                //Debug.Log("判定错误");
                //Vector2 originPos = missEffect.anchoredPosition;
                // DOTween.Sequence().Append(missEffect.DOAnchorPos(new Vector2(missEffect.anchoredPosition.x, 0), 0.6f).SetEase(Ease.InCubic))
                // .Append(missEffect.DOAnchorPos(new Vector2(missEffect.anchoredPosition.x, -250), 0.6f).SetEase(Ease.OutCubic))
                // .Append(missEffect.DOAnchorPos(originPos, 0));
            }




        }
        if (Input.GetKeyDown(KeyCode.Space) && flyingBird.activeSelf){
            birdAudioSource.mute = false;
            DOTween.Sequence().Append(perfectEffect.transform.DOScale(new Vector3(4, 4, 4), 0.3f).SetEase(Ease.OutBack))
                    .Append(perfectEffect.transform.DOScale(new Vector3(0, 0, 0), 0.2f));
        }
    }

    public void PlayChorsClip()
    {
        birdAudioSource.Play();
    }
    void FlyBird()
    {
        StartCoroutine(FlyBirdCoroutine());
    }

    IEnumerator FlyBirdCoroutine(){
        while(birdIndex < birds.Count){
            Instantiate(birds[birdIndex], transform, true);

            birdIndex++;

            yield return new WaitForSeconds(1f);
        }
    }

    public void Miss()
    {
        DOTween.Sequence().Append(missEffect.transform.DOScale(new Vector3(4, 4, 4), 0.3f).SetEase(Ease.OutBack))
            .Append(missEffect.transform.DOScale(new Vector3(0, 0, 0), 0.2f));
        AudioSource missAudio = missEffect.GetComponent<AudioSource>();
        if(!missAudio.isPlaying)
            missAudio.Play();

        barScore--;
    }
}
