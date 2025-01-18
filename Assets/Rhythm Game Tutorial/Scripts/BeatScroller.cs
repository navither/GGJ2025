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

    // Start is called before the first frame update
    void Start()
    {
        beatTempo /= 60f;    
        startPos = transform.GetChild(0).transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted){
            if(Input.anyKeyDown){
                hasStarted = true;
                StartBeating();
            }
        }else{
            transform.RotateAround(transform.localPosition, Vector3.back, beatTempo * 45 * Time.deltaTime);
            //transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }


    }

    void StartBeating(){
        StartCoroutine(BeatingCoroutine());
    }

    IEnumerator BeatingCoroutine(){
        while (true)
        {
            GameObject note = GameObject.Instantiate<GameObject>(notePrefab, startPos, Quaternion.identity, transform);
            note.transform.rotation = notePrefab.transform.rotation;

            counter++;

            if(counter >= 4){
                break;
            }

            yield return new WaitForSeconds(1f);  // 等待1秒
        }
    }
}
