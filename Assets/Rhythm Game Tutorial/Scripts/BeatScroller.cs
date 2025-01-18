using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;

    public bool hasStarted;

    [SerializeField]
    private GameObject notePrefab;

    Vector3 startPos;

    int counter = 0;

    int bar = 0;

    int currentScore = 0;

    //谱子数组
    public int [,] musicNotes = {
        {1, 1, 0, 1}, // 4拍的谱子
        {1, 1, 0, 1},
        {1, 1, 1, 1},
        {1, 1, 0, 1},
        {1, 1, 1, 1},
        {1, 1, 1, 1},
        {1, 1, 1, 1},
        {1, 1, 1, 1},
    };

    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //beatTempo /= 60f;    
        startPos = transform.GetChild(0).transform.position;

        rotationSpeed = (60f / beatTempo) * 2f; 
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted){
            if(Input.anyKeyDown){
                hasStarted = true;
                ContinueBeating();
            }
        }else{
            transform.RotateAround(transform.localPosition, Vector3.back, 90f / rotationSpeed * Time.deltaTime);
            //transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }


    }

    void StartBeating(){
        StartCoroutine(BeatingCoroutine());
    }

    IEnumerator BeatingCoroutine(){
        while (true)
        {
            if(bar >= musicNotes.GetLength(0)){
                bar = 0;
            }
            if(musicNotes[bar, counter] == 1){
                GameObject note = GameObject.Instantiate<GameObject>(notePrefab, startPos, Quaternion.identity, transform);
                note.transform.rotation = notePrefab.transform.rotation;
            }

            counter++;

            if(counter >= 4){
                FinishOneBar();
                break;
            }

            yield return new WaitForSeconds(rotationSpeed);  // 等待rotationSpeed秒, 就是一拍的时间
        }
    }

    void ContinueBeating(){
        StartCoroutine(ContinueBeatingCoroutine());
    }

    IEnumerator ContinueBeatingCoroutine(){
        while (true)
        {
            counter = 0;
            bar++;
            StartCoroutine(BeatingCoroutine());
            yield return new WaitForSeconds(rotationSpeed * 4);  // 等待8拍的时间
        }
    }

    //结束一bar
    void FinishOneBar()
    {
        BeatChecker.instance.barScore = BeatChecker.instance.score - currentScore;
        currentScore = BeatChecker.instance.score;
        
        if(BeatChecker.instance.barScore >= 3){
            //GameEvents.SetBuddleRadius(Bubble.instance.transform.localScale.x + 1);
            DOTween.Sequence().Append(Bubble.instance.transform.DOScale(new Vector3(1,1,1), 0.3f).SetEase(Ease.OutBack).SetRelative());
        }
    }
}
