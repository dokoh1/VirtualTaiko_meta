using UnityEngine;

namespace Single
{
    public class GameStateMachine : MonoBehaviour
    {
        public static GameStateMachine instance { get; private set; }

        private IGameState _currentState;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            ChangeState(new StartState());
        }

        private void Update()
        {
            _currentState?.Execute();
        }

        public void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}