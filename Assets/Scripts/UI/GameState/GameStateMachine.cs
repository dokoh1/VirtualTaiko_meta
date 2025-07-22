using UnityEngine;

namespace Single
{
    public class GameStateMachine : MonoBehaviour
    {
        public static GameStateMachine instance { get; private set; }
        [SerializeField] private StartController startController;
        [SerializeField] private ChoicePresenter choicePresenter;
        [SerializeField] private ResultController resultController;

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
            ChangeStartState();
        }

        private void Update()
        {
            _currentState?.Execute();
        }

        public void ChangeToMusicChoiceState()
        {
            ChangeState(new MusicChoiceState(choicePresenter));
        }

        public void ChangeStartState()
        {
            ChangeState(new StartState(startController));
        }

        public void ChanageResultState(ScoreData scoreData)
        {
            ChangeState(new ResultState(scoreData, resultController));
        }
        public void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}