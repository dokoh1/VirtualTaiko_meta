using UnityEngine;

public class InputManager : MonoBehaviour
{

    public bool isVREnabled; // VR 모드를 우선으로 설정

    private IInputProvider _inputProvider;

    private void Awake()
    {
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
