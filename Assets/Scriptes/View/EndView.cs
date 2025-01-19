using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndView : MonoBehaviour
{

    Button _restartButton;
    Button _exitButton;

    Animator _animator;

    Text _score;

    void Awake()
    {
        _restartButton = transform.Find("RestartButton").GetComponent<Button>();
        _exitButton = transform.Find("ExitButton").GetComponent<Button>();
        _score = transform.Find("Score/ScoreValue").GetComponent<Text>();


        _animator = GetComponent<Animator>();

        StartCoroutine(ShowName());
    }

    IEnumerator ShowName()
    {
        yield return new WaitForSeconds(10);

        _animator.Play("ShowName");
    }

    private void OnEnable()
    {
        UIEvents.CloseEndView += UIEvents_CloseEndView;
        _restartButton.onClick.AddListener(OnRestartButton);
        _exitButton.onClick.AddListener(OnExitButton);

        UIEvents.SetEndGameScore += UIEvents_SetEndGameScore;
    }


    private void OnDisble()
    {
        UIEvents.CloseStartView -= UIEvents_CloseEndView;

        UIEvents.SetEndGameScore -= UIEvents_SetEndGameScore;

    }

    private void UIEvents_SetEndGameScore(int score)
    {
        _score.text = score.ToString();
    }

    private void UIEvents_CloseEndView()
    {
        //gameObject.SetActive(false);

        Destroy(gameObject);
    }

    private void OnRestartButton()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;


        SceneManager.LoadScene(currentSceneName);  


        

        //UIEvents.CloseStartView?.Invoke();
        //UIEvents.OpenGameplayView?.Invoke();
    }

    private void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Update is called once per frame

}
