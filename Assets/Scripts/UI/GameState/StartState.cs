using Object = UnityEngine.Object;

public class StartState : IGameState
{
    private StartController _startController;
    private bool _inputReceived = false;

    public void Enter()
    {
        _startController = Object.FindObjectOfType<StartController>();
        if (_startController != null)
        {
            // _startController.gameObject.SetActive(true);
            _startController.OnInputReceived += HandleInputReceived;
        }
        Single.System.SceneManager.LoadScene(SceneDataType.Start);
    }

    public void Exit()
    {
        if (_startController != null)
        {
            _startController.OnInputReceived -= HandleInputReceived;
            // _startController.gameObject.SetActive(false);
        }
    }

    public void Execute()
    {   
        
    }
    private void HandleInputReceived()
    {
        if (!_inputReceived)
        {
            _inputReceived = true;
            
            Single.GameStateMachine.instance.ChangeState(new MusicChoiceState());
        }
    }

}
