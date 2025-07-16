using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceModel : MonoBehaviour
{
    // 선택 가능한 음악 목록
    [SerializeField] private List<ChoiceData> _choices = new();

    // 현재 활성화된 인덱스
    private int _activeIndex = 3;
    
    public ChoiceData ActiveChoice => _choices[_activeIndex];
    public List<ChoiceData> AllChoices => _choices;

    public event Action<ChoiceData> OnActiveChoiceChanged;
    public event Action<ChoiceType> OnChoiceMade;

    private void Awake()
    {
        OnActiveChoiceChanged?.Invoke(ActiveChoice);
    }

    public void ScrollUp()
    {
        _activeIndex = (_activeIndex - 1 + _choices.Count) % _choices.Count;
        UpdateChoicesOrder(true);
        OnActiveChoiceChanged?.Invoke(ActiveChoice);
    }

    public void ScrollDown()
    {
        _activeIndex = (_activeIndex + 1) % _choices.Count;
        UpdateChoicesOrder(false);
        OnActiveChoiceChanged?.Invoke(ActiveChoice);
    }

    public void MakeChoice()
    {
        OnChoiceMade?.Invoke(ActiveChoice.ChoiceType);
    }
    private void UpdateChoicesOrder(bool scrollUp)
    {
        if (scrollUp)
        {
            ChoiceData last = _choices[^1];
            _choices.RemoveAt(_choices.Count - 1);
            _choices.Insert(0, last);
        }
        else
        {
            ChoiceData first = _choices[0];
            _choices.RemoveAt(0);
            _choices.Add(first);
        }
            
    }
}