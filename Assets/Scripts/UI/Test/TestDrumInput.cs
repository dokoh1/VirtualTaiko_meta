using UnityEngine;

public class TestDrumInput : MonoBehaviour
{
    public DrumDataType testType;
    void Start()
    {
        testType = DrumDataType.NotPlayed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            testType = DrumDataType.LeftFace;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            testType = DrumDataType.RightFace;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            testType = DrumDataType.LeftSide;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            testType = DrumDataType.RightSide;
        else if (!Input.anyKey)
            testType = DrumDataType.NotPlayed;
    }
}
