using UnityEngine;

namespace dokoh
{ 
    public class System : MonoBehaviour
    {
        public static SceneManager SceneManager => Instance.sceneManger;
        [SerializeField] private SceneManager sceneManger;
        
        private static System Instance { get; set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
