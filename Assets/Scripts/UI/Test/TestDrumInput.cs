using UnityEngine;
using UnityEngine.Serialization;

public class TestDrumInput : MonoBehaviour
{
    public DrumInputDataType testInputType;
    public JudgementDataType judgementData;
    void Start()
    {
        testInputType = DrumInputDataType.NotPlayed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            testInputType = DrumInputDataType.LeftFace;
        else if (Input.GetKeyUp(KeyCode.Alpha2))
            testInputType = DrumInputDataType.RightFace;
        else if (Input.GetKeyUp(KeyCode.Alpha3))
            testInputType = DrumInputDataType.LeftSide;
        else if (Input.GetKeyUp(KeyCode.Alpha4))
            testInputType = DrumInputDataType.RightSide;
        else if (Input.GetKeyUp(KeyCode.Alpha5))
            judgementData = JudgementDataType.Good;
        else if (Input.GetKeyUp(KeyCode.Alpha6))
            judgementData = JudgementDataType.Great;
        else if (Input.GetKeyUp(KeyCode.Alpha7))
            judgementData = JudgementDataType.Bad;
        else if (!Input.anyKey)
        {
            testInputType = DrumInputDataType.NotPlayed;
            judgementData = JudgementDataType.None;
        }
    }
}
