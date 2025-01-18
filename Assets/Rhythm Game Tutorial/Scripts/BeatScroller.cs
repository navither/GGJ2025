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

    int bar = -1;

    [SerializeField]
    int currentScore = 0;

    //最小是八分音符
    const int minumBeat = 8;

    //谱子数组
    public int [,] musicNotes = { // 1/4拍一个index， 一列是两拍
        // {0, 0, 0, 0,},
        {0, 0, 0, 0}, //2
        {1, 0, 0, 0}, //3
        {0, 0, 0, 0}, //4
        {1, 0, 0, 1}, //5
        {0, 0, 0, 0}, //6
        {1, 0, 0, 1}, //7
        {0, 0, 0, 0}, //8
        {1, 0, 0, 1}, //9
        {0, 0, 0, 1}, //10
        {1, 0, 0, 1}, //11
        {0, 0, 0, 1}, //12
        {1, 0, 0, 1}, //13
        {0, 0, 0, 1}, //14
        {1, 0, 0, 1}, //15
        {0, 0, 0, 1}, //16
        {1, 0, 0, 1}, //17
        {0, 0, 0, 0}, //18
        {1, 0, 0, 1}, //19
        {0, 1, 0, 0}, //20
        {1, 0, 0, 1}, //21
        {0, 0, 0, 0}, //22
        {1, 0, 0, 1}, //23
        {0, 1, 0, 0}, //24
        {1, 0 ,1, 0}, //25
        {1, 0, 1, 0}, //26
        {1, 0, 1, 0}, //27
        // {1, 1, 1, 1},
        // {1, 1, 1, 1},
    };

    float rotationSpeed;

    List<Transform> rings = new List<Transform>();

    [SerializeField]
    private Transform character;

    // Start is called before the first frame update
    void Start()
    {
        //beatTempo /= 60f;    
        startPos = transform.GetChild(0).transform.position;

        rotationSpeed = (60f / beatTempo); 

        Transform mainRing = transform.Find("main");

        for(int i = 0; i < mainRing.childCount; i++){
            rings.Add(mainRing.GetChild(i));
        }
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
            for(int i = 0; i < rings.Count; i++){
                rings[i].RotateAround(transform.localPosition, Vector3.back, (-10f + i * 2) / rotationSpeed * Time.deltaTime);
            }
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

            if(counter >= 4 ){
                FinishOneBar();
                break;
            }

            yield return new WaitForSeconds(rotationSpeed);  // 等待
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
            DOTween.Sequence().Append(character.DOScale(new Vector3(-0.2f,-0.2f,-0.2f), 0.3f).SetEase(Ease.OutBack).SetRelative())
                .Join(character.DOLocalMoveY(-1f, 0.3f).SetRelative());
        }
    }
}
