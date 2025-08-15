using UnityEngine;
using UnityEngine.Serialization;

namespace Single
{
    public class GameStateMachine : MonoBehaviour
    {
        public static GameStateMachine instance { get; private set; }
        [FormerlySerializedAs("startController")] [SerializeField] private StartView startView;
        [SerializeField] private ChoicePresenter choicePresenter;
        [FormerlySerializedAs("resultController")] [SerializeField] private ResultView resultView;

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
            ChangeState(new StartState(startView));
        }

        public void ChanageResultState(ScoreData scoreData)
        {
            ChangeState(new ResultState(scoreData, resultView));
        }
        public void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}