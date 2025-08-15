
public class PlayState : IGameState
{
    private ChoiceType _selectedMusic;
    public PlayState(ChoiceType selectedMusic)
    {
        _selectedMusic = selectedMusic;
    }
    public void Enter()
    {
        MusicSceneChoice();
        GameEvents.OnGameOver += HandleGameOver;
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        GameEvents.OnGameOver -= HandleGameOver;
    }

    private void MusicSceneChoice()
    {
        switch (_selectedMusic)
        {
            case ChoiceType.Music1:
                Single.System.SceneManager.LoadScene(SceneDataType.Music1);
                break;
            case ChoiceType.Music2:
                Single.System.SceneManager.LoadScene(SceneDataType.Music2);
                break;
            case ChoiceType.Music3:
                Single.System.SceneManager.LoadScene(SceneDataType.Music3);
                break;
        }
    }
    private void HandleGameOver(ScoreData scoreData)
    {
        Single.GameStateMachine.instance.ChanageResultState(scoreData);
    }
}