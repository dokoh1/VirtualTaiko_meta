using UnityEngine;

public class MusicChoiceState : IGameState
{
    public ChoicePresenter ChoicePresenter;

    public MusicChoiceState(ChoicePresenter choicePresenter)
    {
        ChoicePresenter = choicePresenter;
    }
    public void Enter()
    {
        // Debug.Log("Entering Music Choice");
        ChoicePresenter.OnMusicChosen += HandleMusicChosen;
        Single.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
        ChoicePresenter.OnMusicChosen -= HandleMusicChosen;
    }

    public void HandleMusicChosen(ChoiceType choiceType)
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
                Single.GameStateMachine.instance.ChangeStartState();
                break;
        }
    }
}