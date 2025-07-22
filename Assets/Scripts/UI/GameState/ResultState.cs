using UnityEngine;

public class ResultState : IGameState
{
    private ScoreData _finalScoreData;
    private ResultController _resultController;

    public ResultState(ScoreData finalScoreData, ResultController resultController)
    {
        _finalScoreData = finalScoreData;
        _resultController = resultController;
    }
    public void Enter()
    {
        if (_resultController != null)
        {
            _resultController.gameObject.SetActive(true);
        }
        Single.System.SceneManager.LoadScene(SceneDataType.Result);
    }

    public void Execute()
    {
        DrumDataType drumDataType = InputManager.Instance.GetInput();
        if (drumDataType != DrumDataType.NotHit)
        {
            Single.GameStateMachine.instance.ChangeToMusicChoiceState();
        }
    }

    public void Exit()
    {
        if (_resultController != null)
        {
            _resultController.gameObject.SetActive(false);
        }
    }
}