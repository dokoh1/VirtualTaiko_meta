using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Drums DrumFace;
    public DrumSide DrumSide;
    public TestDrumInput Input;
    void Update()
    {
        if (DrumFace.dataSet == DrumDataType.RightFace)
        {
            dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        }
        // if (Input.testInputType == DrumInputDataType.RightFace)
        // {
        //     dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        // }
    }
}
