using UnityEngine;

public class ChoicePresenter : MonoBehaviour
{
    [SerializeField] private ChoiceView _choiceView;
    [SerializeField] private ChoiceModel _choiceModel;

    private void OnEnable(){
        
    }

    private void HandleScrollUpRequested()
    {
        _choiceModel.ScrollUp();
        _choiceView.ScrollUpAnimation();
    }

    private void HandleScrollDownRequested()
    {
        _choiceModel.ScrollDown();
        _choiceView.ScrollDownAnimation();
    }

    private void HandleChoiceMadeRequested()
    {
        // _choiceView.
    }
    
}