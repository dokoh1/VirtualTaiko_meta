using UnityEngine;

public class KeyboardInputProvider : MonoBehaviour, IInputProvider
{
    private DrumDataType _currentInput;

    void Awake()
    {
        _currentInput = DrumDataType.NotHit;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            _currentInput = DrumDataType.LeftFace;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            _currentInput = DrumDataType.RightFace;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            _currentInput = DrumDataType.LeftSide;
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            _currentInput = DrumDataType.DobletFace;
        else
            _currentInput = DrumDataType.NotHit;
    }

    public DrumDataType GetDrumInput()
    {
        return _currentInput;
    }
}
