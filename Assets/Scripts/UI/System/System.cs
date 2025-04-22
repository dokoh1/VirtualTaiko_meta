using UnityEngine;

namespace dokoh
{ 
    public class System : MonoBehaviour
    {
        public static System Instance { get; private set; }

        public static SceneManager SceneManager => Instance?.sceneManager;
        [SerializeField] private SceneManager sceneManager;

        public static ScoreManager ScoreManager => Instance?.scoreManager;
        [SerializeField] private ScoreManager scoreManager;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // 이미 있다면 파괴
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
        }
    }
}