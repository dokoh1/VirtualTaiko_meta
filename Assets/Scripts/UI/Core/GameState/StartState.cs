using UnityEngine;
using Object = UnityEngine.Object;

public class StartState : IGameState
{
    private StartView _startView;
    private bool _inputReceived = false;

    public StartState(StartView startView)
    {
        _startView = startView;
    }
    public void Enter()
    {
        if (_startView != null)
        {
            _startView.OnInputReceived += HandleInputReceived;
        }
        Single.System.SceneManager.LoadScene(SceneDataType.Start);
    }

    public void Exit()
    {
        if (_startView != null)
            _startView.OnInputReceived -= HandleInputReceived;
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
