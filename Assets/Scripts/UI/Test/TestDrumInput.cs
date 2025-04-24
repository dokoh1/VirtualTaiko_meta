using UnityEngine;
using UnityEngine.Serialization;

public class TestDrumInput : MonoBehaviour
{
    public DrumDataType testInputType;
    public JudgementDataType judgementData;
    void Start()
    {
        testInputType = DrumDataType.NotHit;
    }
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            testInputType = DrumDataType.LeftFace;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            testInputType = DrumDataType.RightFace;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            testInputType = DrumDataType.LeftSide;
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            testInputType = DrumDataType.DobletFace;
        else if (Input.GetKeyUp(KeyCode.Alpha5))
            judgementData = JudgementDataType.Good;
        else if (Input.GetKeyUp(KeyCode.Alpha6))
            judgementData = JudgementDataType.Great;
        else if (Input.GetKeyUp(KeyCode.Alpha7))
            judgementData = JudgementDataType.Bad;
        else if (!Input.anyKey)
        {
            testInputType = DrumDataType.NotHit;
            judgementData = JudgementDataType.None;
        }
    }
}
