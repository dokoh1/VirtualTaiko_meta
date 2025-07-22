using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceModel : MonoBehaviour
{
    // 선택 가능한 음악 목록
    [SerializeField] private List<MusicData> _musicChoices = new();

    // 현재 활성화된 인덱스
    private int _activeIndex = 3;
    
    public MusicData ActiveChoice => _musicChoices[_activeIndex];
    public List<MusicData> AllChoices => _musicChoices;
    public int ActiveIndex => _activeIndex;

    public event Action<List<MusicData>> OnChoiceUpdated;


    private void Awake()
    {
        OnChoiceUpdated?.Invoke(_musicChoices);
    }

    public void ScrollUp()
    {
        MusicData top = _musicChoices[0];
        _musicChoices.Remove(top);
        _musicChoices.Add(top);
        OnChoiceUpdated?.Invoke(_musicChoices);
    }

    public void ScrollDown()
    {
        MusicData bottom = _musicChoices[^1];
        _musicChoices.Remove(bottom);
        _musicChoices.Insert(0, bottom);
        OnChoiceUpdated?.Invoke(_musicChoices);
    }
}