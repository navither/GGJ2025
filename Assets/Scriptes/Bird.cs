using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] GameObject _triggerZone;
    [SerializeField] float _flyTime;

    bool _setGravity;

    float _time;

    Vector3 _startPosition;
    Vector3 _endPosition;
    Vector3 _midPosition;

    Rigidbody2D _rigidbody;

    Animator _animator;
    bool testmode;

    bool isDie;

    private void Awake()
    {
        isDie = false;

        _time = 0;
        _flyTime = 1;
        _startPosition = transform.position;
        _endPosition = _triggerZone.transform.position;

        float random = 0.3f + (0.7f - 0.3f) * Random.value;
        _midPosition = Vector3.zero;
        _midPosition.x = Mathf.Lerp(_startPosition.x, _endPosition.x, random);
        _midPosition.y = Mathf.Lerp(_startPosition.y, _endPosition.y, random);

        _midPosition += new Vector3(0, -5 + 10 * Random.value, 0);

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        _setGravity = false;
        _animator = GetComponent<Animator>();
        float a = Random.value;


        if (a > Random.value)
        {
            testmode = true;
        }
        else
        {
            testmode = false;
        }
        //Debug.Log(a);
    }

    private void OnEnable()
    {
        GameEvents.SetIsDie += GameEvents_SetIsDie;
    }


    private void OnDisble()
    {
        GameEvents.SetIsDie -= GameEvents_SetIsDie;
    }

    private void GameEvents_SetIsDie(bool obj)
    {
        isDie = true;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time <= _flyTime)
        {
            float t = _time / _flyTime;

            Vector3 newPosition = (1 - t) * (1 - t) * _startPosition + 2 * (1 - t) * t * _midPosition + t * t * _endPosition;

            if ((newPosition - transform.position).x > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            transform.position = newPosition;

        }
        else
        {

            if (!isDie)
            {
                _rigidbody.gravityScale = 1;
                float xDirection = -_startPosition.x / Mathf.Abs(_startPosition.x);

                _rigidbody.AddForce(new Vector3(xDirection, -5f + 10 * Random.value, 0) * 5);

            }
            else
            {
                _animator.Play("Die");

                _rigidbody.gravityScale = 1;
                float xDirection = -_startPosition.x / Mathf.Abs(_startPosition.x);

                _rigidbody.AddForce(new Vector3(-xDirection, -5f + 5 * Random.value, 0) * 5);

            }
        }

    }


}
