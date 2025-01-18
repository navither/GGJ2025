using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = ("Data/MusicScoreSO"), fileName = ("MusicScoreSO"))]
public class MusicScoreSO : ScriptableObject
{
    [SerializeField] private List<NoteListSO> _allNotes;
    [SerializeField] private AudioSource _audioSource;

    public List<NoteListSO> AllNotes => _allNotes;
    public AudioSource AudioSource => _audioSource;
}
