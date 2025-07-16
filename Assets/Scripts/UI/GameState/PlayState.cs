
public class PlayState : IGameState
{
    private ChoiceType _selectedMusic;

    public PlayState(ChoiceType selectedMusic)
    {
        _selectedMusic = selectedMusic;
    }
    public void Enter()
    {
        Single.System.SceneManager.LoadScene(SceneDataType.Music1);
        GameEvents.OnGameOver += HandleGameOver;
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {
        GameEvents.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver(ScoreData scoreData)
    {
        Single.GameStateMachine.instance.ChangeState(new ResultState(scoreData));
    }
}