using UnityEngine;

public class ResultState : IGameState
{
    private ScoreData _finalScoreData;
    private ResultView _resultView;

    public ResultState(ScoreData finalScoreData, ResultView resultView)
    {
        _finalScoreData = finalScoreData;
        _resultView = resultView;
    }
    public void Enter()
    {
        Single.System.SceneManager.LoadScene(SceneDataType.Result);
        if (_resultView != null)
        {
            _resultView.gameObject.SetActive(true);
            _resultView.Initialize(_finalScoreData);
        }
    }

    public void Execute()
    {
        DrumDataType drumDataType = Single.System.InputManager.GetInput();
        if (drumDataType != DrumDataType.NotHit)
        {
            Single.GameStateMachine.instance.ChangeToMusicChoiceState();
        }
    }

    public void Exit()
    {
        if (_resultView != null)
        {
            _resultView.gameObject.SetActive(false);
        }
    }
}