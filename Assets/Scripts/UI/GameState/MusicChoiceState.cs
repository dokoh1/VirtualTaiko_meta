using UnityEngine;

public class MusicChoiceState : IGameState
{
    private ChoiceView _choiceView;
    public void Enter()
    {
        _choiceView = GameObject.FindObjectOfType<ChoiceView>();
        Single.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        if (_choiceView != null)
        {
            // _choiceScroll.gameObject.SetActive(true);
            _choiceView.OnScrollUpRequested += HandleViewUp;
            _choiceView.OnScrollDownRequested += HandleViewDown;
            _choiceView.OnChoiceMadeRequested += HandleChoiceMade;
        }
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        if (_choiceView != null)
        {
            _choiceView.OnScrollUpRequested -= HandleViewUp;
            _choiceView.OnScrollDownRequested -= HandleViewDown;
            _choiceView.OnChoiceMadeRequested -= HandleChoiceMade;
            // _choiceScroll.gameObject.SetActive(false);
        }
    }

    private void HandleViewUp()
    {
        Debug.Log("Scroll up");
    }

    private void HandleViewDown()
    {
        Debug.Log("Scroll down");
    }

    private void HandleChoiceMade(ChoiceType choiceType)
    {
        switch (choiceType)
        {
            case ChoiceType.Music1:
            case ChoiceType.Music2:
            case ChoiceType.Music3:
            case ChoiceType.RandomMusic:
                Single.GameStateMachine.instance.ChangeState(new PlayState(choiceType));
                break;
            case ChoiceType.BackToMenu:
                Single.GameStateMachine.instance.ChangeState(new StartState());
                break;
        }
    }
}