using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public bool isVREnabled = true; // VR 모드를 우선으로 설정

    private IInputProvider _inputProvider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetupInputProvider();
    }

    private void SetupInputProvider()
    {
        if (isVREnabled)
        {
            _inputProvider = gameObject.AddComponent<VRInputProvider>();
        }
        else
        {
            _inputProvider = gameObject.AddComponent<KeyboardInputProvider>();
        }
    }

    public DrumDataType GetInput()
    {
        return _inputProvider.GetDrumInput();
    }
}
