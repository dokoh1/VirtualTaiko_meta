using UnityEngine;

public class ResultState : IGameState
{
    private ScoreData _finalScoreData;
    private ResultController _resultControllerController;

    public ResultState(ScoreData finalScoreData)
    {
        _finalScoreData = finalScoreData;
    }
    public void Enter()
    {
        _resultControllerController = GameObject.FindObjectOfType<ResultController>();
        if (_resultControllerController != null)
        {
            _resultControllerController.gameObject.SetActive(true);
        }
        Single.System.SceneManager.LoadScene(SceneDataType.Result);
    }

    public void Execute()
    {
        DrumDataType drumDataType = InputManager.Instance.GetInput();
        if (drumDataType != DrumDataType.NotHit)
        {
            Single.GameStateMachine.instance.ChangeState(new MusicChoiceState());
        }
    }

    public void Exit()
    {
        if (_resultControllerController != null)
        {
            _resultControllerController.gameObject.SetActive(false);
        }
    }
}