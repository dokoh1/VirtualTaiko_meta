using UnityEngine;
using Object = UnityEngine.Object;

public class StartState : IGameState
{
    private StartController _startController;
    private bool _inputReceived = false;

    public StartState(StartController startController)
    {
        _startController = startController;
    }
    public void Enter()
    {
        if (_startController != null)
        {
            _startController.OnInputReceived += HandleInputReceived;
        }
        Single.System.SceneManager.LoadScene(SceneDataType.Start);
    }

    public void Exit()
    {
        if (_startController != null)
            _startController.OnInputReceived -= HandleInputReceived;
    }

    public void Execute()
    {   
        
    }
    private void HandleInputReceived()
    {
        if (!_inputReceived)
        {
            _inputReceived = true;
            
            Single.GameStateMachine.instance.ChangeToMusicChoiceState();
        }
    }

}
