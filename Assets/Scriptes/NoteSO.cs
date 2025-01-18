using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;
[System.Serializable]

public class NoteSO
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _nextTime;

    public float SpawnTime => _spawnTime;
    public float NextTime => _nextTime;
}

