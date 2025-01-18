using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

[CreateAssetMenu(menuName = ("Data/NoteListSO"), fileName = ("NoteListSO"))]
public class NoteListSO : ScriptableObject
{
    [SerializeField] private List<Note> _noteList;

    public List<Note> NoteList => _noteList;
    public int NoteListCount => _noteList.Count;
}

