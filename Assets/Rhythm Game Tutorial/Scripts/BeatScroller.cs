using System.Collections;
using System.Collections.Generic;
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

    //谱子数组
    public int [,] musicNotes = {
        {1, 1, 0, 1}, // 4拍的谱子
        {1, 1, 1, 0},
        {1, 1, 1, 1},
        {0, 1, 1, 0},
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
    void FixedUpdate()
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
            if(musicNotes[bar, counter] == 1){
                GameObject note = GameObject.Instantiate<GameObject>(notePrefab, startPos, Quaternion.identity, transform);
                note.transform.rotation = notePrefab.transform.rotation;
            }

            counter++;

            if(counter >= 4){
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
}
