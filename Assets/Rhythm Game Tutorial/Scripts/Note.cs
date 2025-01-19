using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音符碰撞到判定点的时候，就可以进行判定
/// </summary>
public class Note : MonoBehaviour
{
    Collider2D noteCollider2D;

    private void Start() {
        noteCollider2D = GetComponent<Collider2D>();
        noteCollider2D.enabled = false;
        StartCoroutine(LateActivate());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Activator")) {
            // 判定开始
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            sr.color = Color.green;

            BeatChecker.instance.isTiming = true;

            BeatChecker.instance.currentNote = this.gameObject;

            //Debug.Log(Time.time - GameManager.beginTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Activator")) {
            // 判定结束
            //BeatChecker.instance.Miss();
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            sr.color = Color.white;

            BeatChecker.instance.isTiming = false;
            Destroy(gameObject);
        }
    }

    //因为音符会出生在判定点，所以需要等待一点时间再激活碰撞体
    IEnumerator LateActivate(){
        yield return new WaitForSeconds(0.8f);
        noteCollider2D.enabled = true;
    }
}
