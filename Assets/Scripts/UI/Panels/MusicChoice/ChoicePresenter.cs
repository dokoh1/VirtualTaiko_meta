using System;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePresenter : MonoBehaviour
{
    [SerializeField] private ChoiceView _choiceView;
    [SerializeField] private ChoiceModel _choiceModel;
    public event Action<ChoiceType> OnMusicChosen;
    
    private void OnEnable()
    {
        _choiceView.OnScrollUpRequested += HandleScrollUpRequested;
        _choiceView.OnScrollDownRequested += HandleScrollDownRequested;
        _choiceView.OnChoiceMadeRequested += HandleChoiceMadeRequested;

        _choiceModel.OnChoiceUpdated += HandleChoicesUpdated;
    }

    private void OnDisable()
    {
        _choiceView.OnScrollUpRequested -= HandleScrollUpRequested;
        _choiceView.OnScrollDownRequested -= HandleScrollDownRequested;
        _choiceView.OnChoiceMadeRequested -= HandleChoiceMadeRequested;
        
        _choiceModel.OnChoiceUpdated -= HandleChoicesUpdated;
    }

    private void HandleScrollUpRequested()
    {
        _choiceView.PlayScrollUpAnimation(() =>
        {
            _choiceModel.ScrollUp();
        });
    }

    private void HandleScrollDownRequested()
    {
        _choiceView.PlayScrollDownAnimation(() =>
        {
            _choiceModel.ScrollDown();
        });
    }

    private void HandleChoiceMadeRequested()
    {
        _choiceView.PlayChoiceAnimation(() =>
        {
            MusicData selectedChoice = _choiceModel.ActiveChoice;
            OnMusicChosen?.Invoke(selectedChoice.ChoiceType);
        });
    }

    private void HandleChoicesUpdated(List<MusicData> updateChoices)
    {
        _choiceView.UpdateChoiceDisplay(updateChoices);
    }
}